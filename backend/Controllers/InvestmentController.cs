using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApacStellar2026.DatabaseCtx;
using Microsoft.EntityFrameworkCore;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvestmentController : ControllerBase
{
    private readonly ApplicationDatabaseCtx _dbCtx;

    public InvestmentController(ApplicationDatabaseCtx dbCtx)
    {
        _dbCtx = dbCtx;
    }

    [HttpGet]
    public async Task<IActionResult> GetInvestments()
    {
        return Ok(await _dbCtx.Investment.ToListAsync());
    }
}