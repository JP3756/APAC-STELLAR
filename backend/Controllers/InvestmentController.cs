using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ApacStellar2026.Dto.InvestmentDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InvestmentController : ControllerBase
{
    private readonly IInvestmentService _investmentService;

    public InvestmentController(IInvestmentService investmentService)
    {
        _investmentService = investmentService;
    }

    private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetInvestments()
    {
        var userId = GetCurrentUserId();
        var result = await _investmentService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetInvestmentById(int id)
    {
        var userId = GetCurrentUserId();
        var result = await _investmentService.GetByIdAndUserIdAsync(id, userId);
        if (result == null)
        {
            return NotFound(new { message = $"Investment with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvestment([FromBody] InvestmentCreateDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Investment data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _investmentService.CreateAsync(request, userId);
        if (result == null)
        {
            return NotFound(new { message = $"Connected account with ID {request.AccountId} not found." });
        }

        return CreatedAtAction(nameof(GetInvestmentById), new { id = result.InvestmentId }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateInvestment(int id, [FromBody] UpdateInvestmentDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Investment data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _investmentService.UpdateAsync(id, request, userId);
        if (result == null)
        {
            return NotFound(new { message = $"Investment with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteInvestment(int id)
    {
        var userId = GetCurrentUserId();
        var deleted = await _investmentService.DeleteAsync(id, userId);
        if (!deleted)
        {
            return NotFound(new { message = $"Investment with ID {id} not found." });
        }

        return NoContent();
    }
}