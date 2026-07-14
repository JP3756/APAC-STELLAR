using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace ApacStellar2026.Settings;

internal sealed class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var bearerScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Enter your JWT token"
        };
        
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes["Bearer"] = bearerScheme;
        
        return Task.CompletedTask;
    }
}