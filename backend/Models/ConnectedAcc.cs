using ApacStellar2026.Models;
using System.ComponentModel.DataAnnotations;

namespace ApacStellar2026.Models;

public class ConnectedAccount
{
    [Key]
    public int AccountId { get; set; }

    public ApplicationUser? User { get; set; }

    public int InstitutionId { get; set; }

    public FinancialInstitution FinancialInstitution { get; set; }

    public int AccountType { get; set; }

    public int Status { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public ICollection<Investment> Investments { get; set; } = new List<Investment>();
}
