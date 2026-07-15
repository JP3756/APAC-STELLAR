using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ApacStellar2026.Dto.StellarWalletDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StellarWalletController : ControllerBase
{
    private readonly IStellarWalletService _stellarWalletService;

    public StellarWalletController(IStellarWalletService stellarWalletService)
    {
        _stellarWalletService = stellarWalletService;
    }

    private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetStellarWallets()
    {
        var userId = GetCurrentUserId();
        var result = await _stellarWalletService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStellarWalletById(int id)
    {
        var userId = GetCurrentUserId();
        var result = await _stellarWalletService.GetByIdAndUserIdAsync(id, userId);
        if (result == null)
        {
            return NotFound(new { message = $"StellarWallet with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetStellarWalletsByUserId(string userId)
    {
        var currentUserId = GetCurrentUserId();
        if (userId != currentUserId)
        {
            return Forbid();
        }

        var result = await _stellarWalletService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStellarWallet([FromBody] StellarWalletCreateDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "StellarWallet data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _stellarWalletService.CreateAsync(request, userId);
        return CreatedAtAction(nameof(GetStellarWalletById), new { id = result.StellarWalletId }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStellarWallet(int id, [FromBody] UpdateStellarWalletDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "StellarWallet data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _stellarWalletService.UpdateAsync(id, request, userId);
        if (result == null)
        {
            return NotFound(new { message = $"StellarWallet with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStellarWallet(int id)
    {
        var userId = GetCurrentUserId();
        var deleted = await _stellarWalletService.DeleteAsync(id, userId);
        if (!deleted)
        {
            return NotFound(new { message = $"StellarWallet with ID {id} not found." });
        }

        return NoContent();
    }

    [HttpGet("{id:int}/balance")]
    public async Task<IActionResult> GetBalance(int id)
    {
        var userId = GetCurrentUserId();
        var wallet = await _stellarWalletService.GetByIdAndUserIdAsync(id, userId);
        if (wallet == null)
        {
            return NotFound(new { message = $"StellarWallet with ID {id} not found." });
        }

        var balance = await _stellarWalletService.GetBalanceAsync(id);
        return Ok(new { stellarWalletId = id, balance });
    }
}