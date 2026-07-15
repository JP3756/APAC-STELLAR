using Microsoft.AspNetCore.Mvc;
using ApacStellar2026.Dto.LoanDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLoans()
    {
        var result = await _loanService.GetAllLoansAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetLoanById(int id)
    {
        var result = await _loanService.GetLoanByIdAsync(id);
        if (result == null)
        {
            return NotFound(new { message = $"Loan with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLoan([FromBody] LoanCreateDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Loan data is null." });
        }

        var result = await _loanService.CreateLoanAsync(request);
        return CreatedAtAction(nameof(GetLoanById), new { id = result.LoanId }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLoan(int id, [FromBody] UpdateLoanDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Loan data is null." });
        }

        var result = await _loanService.UpdateLoanAsync(id, request);
        if (result == null)
        {
            return NotFound(new { message = $"Loan with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var deleted = await _loanService.DeleteLoanAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Loan with ID {id} not found." });
        }

        return NoContent();
    }
}