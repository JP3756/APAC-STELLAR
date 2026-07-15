using ApacStellar2026.Models;
using Microsoft.AspNetCore.Identity;

namespace ApacStellar2026.Service;

public class IdentitySeederService {
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentitySeederService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedRolesAsync()
    {
        string[] roles = ["User", "Admin", "Business"];

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}