using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ApacStellar2026.Dto.ConnectedAccDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConnectedAccController : ControllerBase
{
    private readonly IConnectedAccService _connectedAccService;

    public ConnectedAccController(IConnectedAccService connectedAccService)
    {
        _connectedAccService = connectedAccService;
    }

    private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetConnectedAccounts()
    {
        var userId = GetCurrentUserId();
        var result = await _connectedAccService.GetAllByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetConnectedAccountById(int id)
    {
        var userId = GetCurrentUserId();
        var result = await _connectedAccService.GetByIdAndUserIdAsync(id, userId);
        if (result == null)
        {
            return NotFound(new { message = $"Connected account with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateConnectedAccount([FromBody] ConnectedAccCreateDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Connected account data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _connectedAccService.CreateAsync(request, userId);
        return CreatedAtAction(nameof(GetConnectedAccountById), new { id = result.AccountId }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateConnectedAccount(int id, [FromBody] UpdateConnectedAccDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Connected account data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _connectedAccService.UpdateAsync(id, request, userId);
        if (result == null)
        {
            return NotFound(new { message = $"Connected account with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteConnectedAccount(int id)
    {
        var userId = GetCurrentUserId();
        var deleted = await _connectedAccService.DeleteAsync(id, userId);
        if (!deleted)
        {
            return NotFound(new { message = $"Connected account with ID {id} not found." });
        }

        return NoContent();
    }
}