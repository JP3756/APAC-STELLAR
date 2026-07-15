using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ApacStellar2026.Dto.TransactionDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        var userId = GetCurrentUserId();
        var result = await _transactionService.GetTransactionsByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        var userId = GetCurrentUserId();
        var result = await _transactionService.GetByIdAndUserIdAsync(id, userId);
        if (result == null)
        {
            return NotFound(new { message = $"Transaction with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] TransactionCreateDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Transaction data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _transactionService.CreateAsync(request, userId);
        if (result == null)
        {
            return NotFound(new { message = "Associated loan or investment not found." });
        }

        return CreatedAtAction(nameof(GetTransactionById), new { id = result.TransactionId }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTransaction(int id, [FromBody] UpdateTransactionDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Transaction data is null." });
        }

        var userId = GetCurrentUserId();
        var result = await _transactionService.UpdateAsync(id, request, userId);
        if (result == null)
        {
            return NotFound(new { message = $"Transaction with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var userId = GetCurrentUserId();
        var deleted = await _transactionService.DeleteAsync(id, userId);
        if (!deleted)
        {
            return NotFound(new { message = $"Transaction with ID {id} not found." });
        }

        return NoContent();
    }
}