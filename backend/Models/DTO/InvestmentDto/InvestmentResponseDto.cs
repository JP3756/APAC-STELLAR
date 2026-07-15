using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Dto.InvestmentDto;

public class InvestmentResponseDto
{
    public int InvestmentId { get; set; }
    public int AccountId { get; set; }
    public required string AssetName { get; set; }
    public AssetType AssetType { get; set; }
    public int Quantity { get; set; }
    public int MarketValue { get; set; }
    public int Currency { get; set; }
}