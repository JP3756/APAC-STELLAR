using ApacStellar2026.Dto.InvestmentDto;

namespace ApacStellar2026.Interface;

public interface IInvestmentService
{
    Task<List<InvestmentResponseDto>> GetAllAsync();
    Task<InvestmentResponseDto?> GetByIdAsync(int id);
    Task<InvestmentResponseDto> CreateAsync(InvestmentCreateDto dto);
    Task<InvestmentResponseDto?> UpdateAsync(int id, UpdateInvestmentDto dto);
    Task<bool> DeleteAsync(int id);
}