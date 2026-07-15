using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Dto.StellarPaymentDto;

public class StellarPaymentResponseDto
{
    public int StellarPaymentId { get; set; }
    public int StellarWalletId { get; set; }
    public string DestinationAddress { get; set; } = string.Empty;
    public string AssetCode { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Memo { get; set; }
    public string? TransactionHash { get; set; }
    public StellarPaymentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? ErrorMessage { get; set; }
}