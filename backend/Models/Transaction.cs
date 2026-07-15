using System.ComponentModel.DataAnnotations;
using ApacStellar2026.Models.Enums;

namespace ApacStellar2026.Models;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    public int LoanId { get; set; }

    public Loan? Loan { get; set; }

    public int InvestmentId { get; set; }

    public Investment? Investment { get; set; }

    public int Principal { get; set; }

    public int Amount { get; set; }

    public int Currency { get; set; }

    public DateTime Date { get; set; }

    public TransactionType TransactionType { get; set; }

    public bool StellarTxHash { get; set; }

    public bool Description { get; set; }
}
