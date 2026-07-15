using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApacStellar2026.DatabaseCtx;
using Microsoft.EntityFrameworkCore;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ApplicationDatabaseCtx _dbCtx;

    public TransactionController(ApplicationDatabaseCtx dbCtx)
    {
        _dbCtx = dbCtx;
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        return Ok(await _dbCtx.Transaction.ToListAsync());
    }
}