using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ApacStellar2026.Dto.LoanDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetLoans()
    {
        var userId = GetCurrentUserId();
        var result = await _loanService.GetLoansByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetLoanById(int id)
    {
        var userId = GetCurrentUserId();
        var result = await _loanService.GetLoanByIdAndUserIdAsync(id, userId);
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

        var userId = GetCurrentUserId();
        var result = await _loanService.CreateLoanAsync(request, userId);
        if (result == null)
        {
            return NotFound(new { message = $"Connected account with ID {request.AccountId} not found." });
        }

        return CreatedAtAction(nameof(GetLoanById), new { id = result.LoanId }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLoan(int id, [FromBody] UpdateLoanDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Loan data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _loanService.UpdateLoanAsync(id, request, userId);
        if (result == null)
        {
            return NotFound(new { message = $"Loan with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var userId = GetCurrentUserId();
        var deleted = await _loanService.DeleteLoanAsync(id, userId);
        if (!deleted)
        {
            return NotFound(new { message = $"Loan with ID {id} not found." });
        }

        return NoContent();
    }
}