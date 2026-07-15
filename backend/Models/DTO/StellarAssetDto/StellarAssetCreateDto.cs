namespace ApacStellar2026.Dto.StellarAssetDto;

public class StellarAssetCreateDto
{
    public int StellarWalletId { get; set; }
    public string AssetCode { get; set; } = string.Empty;
    public string? AssetIssuer { get; set; }
    public bool IsIssuer { get; set; }
}