using System.ComponentModel.DataAnnotations;

namespace ApacStellar2026.Models;

public class Loan
{ 
    [Key]
    public int LoanId { get; set; }

    public int AccountId { get; set; }

    public ConnectedAccount Account { get; set; }

    public int LoanNumber { get; set; }

    public int Principal { get; set; }

    public int Balance { get; set; }

    public int Interest { get; set; }

    public DateTime DueDate { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
