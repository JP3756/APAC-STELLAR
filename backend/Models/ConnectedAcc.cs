using ApacStellar2026.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApacStellar2026.Models;

public class ConnectedAccount
{
    [Key]
    public int AccountId { get; set; }

    [Required]
    public string ApplicationUserId { get; set; } = string.Empty;

    [ForeignKey(nameof(ApplicationUserId))]
    public ApplicationUser? User { get; set; }

    public int InstitutionId { get; set; }

    public FinancialInstitution? FinancialInstitution { get; set; }

    public AccountType AccountType { get; set; }

    public ConnectionStatus Status { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public ICollection<Investment> Investments { get; set; } = new List<Investment>();
}
