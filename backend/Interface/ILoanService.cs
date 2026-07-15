using ApacStellar2026.Dto.LoanDto;

namespace ApacStellar2026.Interface;

public interface ILoanService
{
    Task<List<LoanResponseDto>> GetAllLoansAsync();
    Task<LoanResponseDto?> GetLoanByIdAsync(int id);
    Task<LoanResponseDto> CreateLoanAsync(LoanCreateDto dto);
    Task<LoanResponseDto?> UpdateLoanAsync(int id, UpdateLoanDto dto);
    Task<bool> DeleteLoanAsync(int id);
}