using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ApacStellar2026.Dto.StellarPaymentDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StellarPaymentController : ControllerBase
{
    private readonly IStellarPaymentService _stellarPaymentService;

    public StellarPaymentController(IStellarPaymentService stellarPaymentService)
    {
        _stellarPaymentService = stellarPaymentService;
    }

    private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetStellarPayments()
    {
        var userId = GetCurrentUserId();
        var result = await _stellarPaymentService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStellarPaymentById(int id)
    {
        var userId = GetCurrentUserId();
        var result = await _stellarPaymentService.GetByIdAndUserIdAsync(id, userId);
        if (result == null)
        {
            return NotFound(new { message = $"StellarPayment with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpGet("wallet/{walletId:int}")]
    public async Task<IActionResult> GetStellarPaymentsByWalletId(int walletId)
    {
        var userId = GetCurrentUserId();
        var result = await _stellarPaymentService.GetByWalletIdAndUserIdAsync(walletId, userId);
        return Ok(result);
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendPayment([FromBody] StellarPaymentCreateDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "StellarPayment data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _stellarPaymentService.SendPaymentAsync(request, userId);
        if (result == null)
        {
            return NotFound(new { message = $"StellarWallet with ID {request.StellarWalletId} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStellarPayment(int id)
    {
        var userId = GetCurrentUserId();
        var deleted = await _stellarPaymentService.DeleteAsync(id, userId);
        if (!deleted)
        {
            return NotFound(new { message = $"StellarPayment with ID {id} not found." });
        }

        return NoContent();
    }
}