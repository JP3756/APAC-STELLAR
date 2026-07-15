using ApacStellar2026.Dto.TransactionDto;

namespace ApacStellar2026.Interface;

public interface ITransactionService
{
    Task<List<TransactionResponseDto>> GetTransactionsByUserIdAsync(string userId);
    Task<TransactionResponseDto?> GetByIdAndUserIdAsync(int id, string userId);
    Task<TransactionResponseDto?> CreateAsync(TransactionCreateDto dto, string userId);
    Task<TransactionResponseDto?> UpdateAsync(int id, UpdateTransactionDto dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}