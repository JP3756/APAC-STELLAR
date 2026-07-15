using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Models;

public class StellarPayment
{
    [Key]
    public int StellarPaymentId { get; set; }

    public int StellarWalletId { get; set; }

    [ForeignKey(nameof(StellarWalletId))]
    public StellarWallet? StellarWallet { get; set; }

    [Required]
    public string DestinationAddress { get; set; } = string.Empty;

    public string AssetCode { get; set; } = "XLM";

    [Column(TypeName = "decimal(18,7)")]
    public decimal Amount { get; set; }

    public string? Memo { get; set; }

    public string? TransactionHash { get; set; }

    public StellarPaymentStatus Status { get; set; } = StellarPaymentStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public string? ErrorMessage { get; set; }
}