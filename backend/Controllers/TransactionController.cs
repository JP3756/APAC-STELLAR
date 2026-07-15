using Microsoft.AspNetCore.Mvc;
using ApacStellar2026.Dto.TransactionDto;
using ApacStellar2026.Interface;

namespace ApacStellar2026.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        var result = await _transactionService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTransactionById(int id)
    {
        var result = await _transactionService.GetByIdAsync(id);
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

        var result = await _transactionService.CreateAsync(request);
        return CreatedAtAction(nameof(GetTransactionById), new { id = result.TransactionId }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTransaction(int id, [FromBody] UpdateTransactionDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Transaction data is null." });
        }

        var result = await _transactionService.UpdateAsync(id, request);
        if (result == null)
        {
            return NotFound(new { message = $"Transaction with ID {id} not found." });
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var deleted = await _transactionService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Transaction with ID {id} not found." });
        }

        return NoContent();
    }
}