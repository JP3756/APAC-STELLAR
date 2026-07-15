using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ApacStellar2026.Dto.StellarAssetDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StellarAssetController : ControllerBase
{
    private readonly IStellarAssetService _stellarAssetService;

    public StellarAssetController(IStellarAssetService stellarAssetService)
    {
        _stellarAssetService = stellarAssetService;
    }

    private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetStellarAssets()
    {
        var userId = GetCurrentUserId();
        var result = await _stellarAssetService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStellarAssetById(int id)
    {
        var userId = GetCurrentUserId();
        var result = await _stellarAssetService.GetByIdAndUserIdAsync(id, userId);
        if (result == null)
        {
            return NotFound(new { message = $"StellarAsset with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpGet("wallet/{walletId:int}")]
    public async Task<IActionResult> GetStellarAssetsByWalletId(int walletId)
    {
        var userId = GetCurrentUserId();
        var result = await _stellarAssetService.GetByWalletIdAndUserIdAsync(walletId, userId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStellarAsset([FromBody] StellarAssetCreateDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "StellarAsset data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _stellarAssetService.CreateAsync(request, userId);
        if (result == null)
        {
            return NotFound(new { message = $"StellarWallet with ID {request.StellarWalletId} not found." });
        }

        return CreatedAtAction(nameof(GetStellarAssetById), new { id = result.StellarAssetId }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStellarAsset(int id, [FromBody] UpdateStellarAssetDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "StellarAsset data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _stellarAssetService.UpdateAsync(id, request, userId);
        if (result == null)
        {
            return NotFound(new { message = $"StellarAsset with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStellarAsset(int id)
    {
        var userId = GetCurrentUserId();
        var deleted = await _stellarAssetService.DeleteAsync(id, userId);
        if (!deleted)
        {
            return NotFound(new { message = $"StellarAsset with ID {id} not found." });
        }

        return NoContent();
    }
}