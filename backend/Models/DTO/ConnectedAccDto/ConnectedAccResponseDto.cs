using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Dto.ConnectedAccDto;

public class ConnectedAccResponseDto
{
    public int AccountId { get; set; }
    public int InstitutionId { get; set; }
    public string? FinancialInstitutionName { get; set; }
    public AccountType AccountType { get; set; }
    public ConnectionStatus Status { get; set; }
}