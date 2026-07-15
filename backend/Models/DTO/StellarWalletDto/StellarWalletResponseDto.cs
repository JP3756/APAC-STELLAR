using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Dto.StellarWalletDto;

public class StellarWalletResponseDto
{
    public int StellarWalletId { get; set; }
    public string ApplicationUserId { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
    public StellarNetwork Network { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Balance { get; set; }
}