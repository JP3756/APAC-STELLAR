using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApacStellar2026.DatabaseCtx;
using Microsoft.EntityFrameworkCore;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly ApplicationDatabaseCtx _dbCtx;

    public LoanController(ApplicationDatabaseCtx dbCtx)
    {
        _dbCtx = dbCtx;
    }

    [HttpGet]
    public async Task<IActionResult> GetLoans()
    {
        return Ok(await _dbCtx.Loan.ToListAsync());
    }
}