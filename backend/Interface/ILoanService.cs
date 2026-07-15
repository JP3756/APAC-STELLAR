using ApacStellar2026.Dto.LoanDto;

namespace ApacStellar2026.Interface;

public interface ILoanService
{
    Task<List<LoanResponseDto>> GetLoansByUserIdAsync(string userId);
    Task<LoanResponseDto?> GetLoanByIdAndUserIdAsync(int id, string userId);
    Task<LoanResponseDto?> CreateLoanAsync(LoanCreateDto dto, string userId);
    Task<LoanResponseDto?> UpdateLoanAsync(int id, UpdateLoanDto dto, string userId);
    Task<bool> DeleteLoanAsync(int id, string userId);
}