using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApacStellar2026.Models;

public class StellarAsset
{
    [Key]
    public int StellarAssetId { get; set; }

    public int StellarWalletId { get; set; }

    [ForeignKey(nameof(StellarWalletId))]
    public StellarWallet? StellarWallet { get; set; }

    [Required]
    public string AssetCode { get; set; } = string.Empty;

    public string? AssetIssuer { get; set; }

    [Column(TypeName = "decimal(18,7)")]
    public decimal Balance { get; set; }

    public bool IsIssuer { get; set; }
}