using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ApacStellar2026.Models;

namespace ApacStellar2026.DatabaseCtx;

public class ApplicationDatabaseCtx : IdentityDbContext<IdentityUser>
{
    public DbSet<ConnectedAccount> ConnectedAccount { get; set; }
    public DbSet<FinancialInstitution> FinancialInstitution { get; set; }
    public DbSet<Investment> Investment { get; set; }
    public DbSet<Loan> Loan { get; set; }
    public DbSet<Transaction> Transaction { get; set; }
    
    public ApplicationDatabaseCtx(DbContextOptions<ApplicationDatabaseCtx> options) : base(options)
    {
    }
}
