namespace ApacStellar2026.Dto.StellarPaymentDto;

public class StellarPaymentCreateDto
{
    public int StellarWalletId { get; set; }
    public string DestinationAddress { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? AssetCode { get; set; }
    public string? Memo { get; set; }
}