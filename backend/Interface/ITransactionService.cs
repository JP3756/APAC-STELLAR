using ApacStellar2026.Dto.TransactionDto;

namespace ApacStellar2026.Interface;

public interface ITransactionService
{
    Task<List<TransactionResponseDto>> GetAllAsync();
    Task<TransactionResponseDto?> GetByIdAsync(int id);
    Task<TransactionResponseDto> CreateAsync(TransactionCreateDto dto);
    Task<TransactionResponseDto?> UpdateAsync(int id, UpdateTransactionDto dto);
    Task<bool> DeleteAsync(int id);
}