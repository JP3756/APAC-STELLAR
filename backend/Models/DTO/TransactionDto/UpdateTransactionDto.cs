namespace ApacStellar2026.Dto.TransactionDto;

public class UpdateTransactionDto
{
    public int LoanId { get; set; }
    public int InvestmentId { get; set; }
    public int Principal { get; set; }
    public int Amount { get; set; }
    public int Currency { get; set; }
    public DateTime Date { get; set; }
    public bool Status { get; set; }
    public bool StellarTxHash { get; set; }
    public bool Description { get; set; }
}