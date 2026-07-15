using System.ComponentModel.DataAnnotations;

namespace ApacStellar2026.Models;

public class FinanctialInstitution
{ 
    [Key]
    public int FinanctialInstitutionId { get; set; }

    public string Name { get; set; }

    public int InstitutionType { get; set; }

    public string ApiEndpoint { get; set; }

    public bool IsStellarSupported { get; set; }

    public ICollection<ConnectedAccount> ConnectedAccounts { get; set; } = new List<ConnectedAccount>();
}
