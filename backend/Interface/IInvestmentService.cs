using ApacStellar2026.Dto.InvestmentDto;

namespace ApacStellar2026.Interface;

public interface IInvestmentService
{
    Task<List<InvestmentResponseDto>> GetByUserIdAsync(string userId);
    Task<InvestmentResponseDto?> GetByIdAndUserIdAsync(int id, string userId);
    Task<InvestmentResponseDto?> CreateAsync(InvestmentCreateDto dto, string userId);
    Task<InvestmentResponseDto?> UpdateAsync(int id, UpdateInvestmentDto dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}