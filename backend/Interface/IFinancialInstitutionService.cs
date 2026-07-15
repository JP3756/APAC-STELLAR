using ApacStellar2026.Dto.FinancialInstitutionDto;

namespace ApacStellar2026.Interface;

public interface IFinancialInstitutionService
{
    Task<List<FinancialInstitutionResponseDto>> GetAllAsync();
    Task<FinancialInstitutionResponseDto?> GetByIdAsync(int id);
    Task<FinancialInstitutionResponseDto> CreateAsync(FinancialInstitutionCreateDto dto);
    Task<FinancialInstitutionResponseDto?> UpdateAsync(int id, UpdateFinancialInstitutionDto dto);
    Task<bool> DeleteAsync(int id);
}