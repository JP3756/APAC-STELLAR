using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApacStellar2026.DatabaseCtx;
using Microsoft.EntityFrameworkCore;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinancialInstitutionController : ControllerBase
{
    private readonly ApplicationDatabaseCtx _dbCtx;

    public FinancialInstitutionController(ApplicationDatabaseCtx dbCtx)
    {
        _dbCtx = dbCtx;
    }

    [HttpGet]
    public async Task<IActionResult> GetFinancialInstitutions()
    {
        return Ok(await _dbCtx.FinancialInstitution.ToListAsync());
    }
}