namespace ApacStellar2026.Dto.TransactionDto;

using ApacStellar2026.Models.Enums;

public class TransactionResponseDto
{
    public int TransactionId { get; set; }
    public int LoanId { get; set; }
    public int InvestmentId { get; set; }
    public int Principal { get; set; }
    public int Amount { get; set; }
    public int Currency { get; set; }
    public DateTime Date { get; set; }
    public TransactionType TransactionType { get; set; }
    public string? StellarTxHash { get; set; }
    public bool Description { get; set; }
}