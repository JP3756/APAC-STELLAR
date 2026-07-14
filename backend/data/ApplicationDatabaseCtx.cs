using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ApacStellar2026.DatabaseCtx;

public class ApplicationDatabaseCtx : IdentityDbContext<IdentityUser>
{
    public ApplicationDatabaseCtx(DbContextOptions<ApplicationDatabaseCtx> options) : base(options)
    {
    }
}
