using Microsoft.AspNetCore.Mvc;
using ApacStellar2026.Dto.InvestmentDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvestmentController : ControllerBase
{
    private readonly IInvestmentService _investmentService;

    public InvestmentController(IInvestmentService investmentService)
    {
        _investmentService = investmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetInvestments()
    {
        var result = await _investmentService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetInvestmentById(int id)
    {
        var result = await _investmentService.GetByIdAsync(id);
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

        var result = await _investmentService.CreateAsync(request);
        return CreatedAtAction(nameof(GetInvestmentById), new { id = result.InvestmentId }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateInvestment(int id, [FromBody] UpdateInvestmentDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Investment data is null." });
        }

        var result = await _investmentService.UpdateAsync(id, request);
        if (result == null)
        {
            return NotFound(new { message = $"Investment with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteInvestment(int id)
    {
        var deleted = await _investmentService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Investment with ID {id} not found." });
        }

        return NoContent();
    }
}