using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Scalar.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
using System.Security.Claims;
using ApacStellar2026.DatabaseCtx;
using ApacStellar2026.Models;
using ApacStellar2026.Settings;
using ApacStellar2026.Interface;
using ApacStellar2026.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<ApplicationDatabaseCtx>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDatabaseCtx>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Identity.Bearer"; 
    options.DefaultChallengeScheme = "Identity.Bearer";
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        RoleClaimType = ClaimTypes.Role
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User", "Admin", "Business"));
    options.AddPolicy("RequireAdminOrBusinessRole", policy => policy.RequireRole("Admin", "Business"));
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(builder.Configuration["Jwt:Audience"] ?? "http://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 10,
                QueueLimit = 10,
                Window = TimeSpan.FromMinutes(1)
            }
        ));
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.Headers["Retry-After"] = "60";
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", cancellationToken);
    };
});

//AutoMapper Config bitch
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
});

// Bind StellarSettings
builder.Services.Configure<StellarSettings>(builder.Configuration.GetSection("StellarSettings"));
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

// Register existing services
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IConnectedAccService, ConnectedAccService>();
builder.Services.AddScoped<IFinancialInstitutionService, FinancialInstitutionService>();
builder.Services.AddScoped<IInvestmentService, InvestmentService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IdentitySeederService>();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();

// Register Stellar services
builder.Services.AddScoped<StellarService>();
builder.Services.AddScoped<IStellarWalletService, StellarWalletService>();
builder.Services.AddScoped<IStellarPaymentService, StellarPaymentService>();
builder.Services.AddScoped<IStellarAssetService, StellarAssetService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.MapIdentityApi<ApplicationUser>();
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }))
   .AllowAnonymous();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeederService>();

    try
    {
        await seeder.SeedRolesAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Role Seeding Failed: {ex.Message}");
    }
}

app.Run();