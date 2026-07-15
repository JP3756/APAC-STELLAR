using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Dto.FinancialInstitutionDto;

public class UpdateFinancialInstitutionDto
{
    public required string Name { get; set; }
    public InstitutionType InstitutionType { get; set; }
    public string? ApiEndpoint { get; set; }
    public bool IsStellarSupported { get; set; }
}