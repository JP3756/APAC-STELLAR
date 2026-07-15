namespace ApacStellar2026.Dto.StellarAssetDto;

public class StellarAssetResponseDto
{
    public int StellarAssetId { get; set; }
    public int StellarWalletId { get; set; }
    public string AssetCode { get; set; } = string.Empty;
    public string? AssetIssuer { get; set; }
    public decimal Balance { get; set; }
    public bool IsIssuer { get; set; }
}