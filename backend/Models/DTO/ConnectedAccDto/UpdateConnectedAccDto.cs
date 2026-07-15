using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Dto.ConnectedAccDto;

public class UpdateConnectedAccDto
{
    public AccountType AccountType { get; set; }
    public ConnectionStatus Status { get; set; }
}