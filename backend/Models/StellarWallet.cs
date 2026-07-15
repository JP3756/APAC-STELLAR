using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Models;

public class StellarWallet
{
    [Key]
    public int StellarWalletId { get; set; }

    [Required]
    public string ApplicationUserId { get; set; } = string.Empty;

    [ForeignKey(nameof(ApplicationUserId))]
    public ApplicationUser? User { get; set; }

    [Required]
    public string PublicKey { get; set; } = string.Empty;

    [Required]
    public string SecretKeyEncrypted { get; set; } = string.Empty;

    public StellarNetwork Network { get; set; } = StellarNetwork.Testnet;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; }

    public ICollection<StellarPayment> Payments { get; set; } = new List<StellarPayment>();
    public ICollection<StellarAsset> Assets { get; set; } = new List<StellarAsset>();
}