using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Dto.ConnectedAccDto;

public class ConnectedAccCreateDto
{
    public int InstitutionId { get; set; }
    public AccountType AccountType { get; set; }
    public ConnectionStatus Status { get; set; }
}