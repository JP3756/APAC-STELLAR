using ApacStellar2026.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApacStellar2026.Models;

public class FinancialInstitution
{
    [Key]
    public int FinancialInstitutionId { get; set; }

    public required string Name { get; set; }

    public InstitutionType InstitutionType { get; set; }

    public string? ApiEndpoint { get; set; }

    public bool IsStellarSupported { get; set; }

    public ICollection<ConnectedAccount> ConnectedAccounts { get; set; } = new List<ConnectedAccount>();
}
