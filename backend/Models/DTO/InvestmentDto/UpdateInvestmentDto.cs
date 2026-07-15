using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Dto.InvestmentDto;

public class UpdateInvestmentDto
{
    public required string AssetName { get; set; }
    public AssetType AssetType { get; set; }
    public int Quantity { get; set; }
    public int MarketValue { get; set; }
    public int Currency { get; set; }
}