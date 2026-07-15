using System.ComponentModel.DataAnnotations;

namespace ApacStellar2026.Models;

public class Investment
{
    [Key]
    public int InvestmentId { get; set; }

    public int AccountId { get; set; }

    public ConnectedAccount? Account { get; set; }

    public required string AssetName { get; set; }

    public int AssetType { get; set; }

    public int Quantity { get; set; }

    public int MarketValue { get; set; }

    public int Currency { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
