using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApacStellar2026.Dto.FinancialInstitutionDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinancialInstitutionController : ControllerBase
{
    private readonly IFinancialInstitutionService _financialInstitutionService;

    public FinancialInstitutionController(IFinancialInstitutionService financialInstitutionService)
    {
        _financialInstitutionService = financialInstitutionService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetFinancialInstitutions()
    {
        var result = await _financialInstitutionService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFinancialInstitutionById(int id)
    {
        var result = await _financialInstitutionService.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound(new { message = $"Financial institution with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Business")]
    public async Task<IActionResult> CreateFinancialInstitution([FromBody] FinancialInstitutionCreateDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Financial institution data is null." });
        }

        var result = await _financialInstitutionService.CreateAsync(request);
        return CreatedAtAction(nameof(GetFinancialInstitutionById), new { id = result.FinancialInstitutionId }, result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Business")]
    public async Task<IActionResult> UpdateFinancialInstitution(int id, [FromBody] UpdateFinancialInstitutionDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Financial institution data is null." });
        }

        var result = await _financialInstitutionService.UpdateAsync(id, request);
        if (result == null)
        {
            return NotFound(new { message = $"Financial institution with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin,Business")]
    public async Task<IActionResult> DeleteFinancialInstitution(int id)
    {
        var deleted = await _financialInstitutionService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Financial institution with ID {id} not found." });
        }

        return NoContent();
    }
}