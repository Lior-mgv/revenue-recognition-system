using System.ComponentModel.DataAnnotations;

namespace FinancesProject.DTO;

public class ContractForm
{
    [Required]
    public required int IdProduct { get; set; }

    [Required]
    public required string Version { get; set; }

    [Required]
    public required int IdClient { get; set; }

    [Required]
    public required DateTime StartDate { get; set; }
    
    [Required]
    public required DateTime EndDate { get; set; }

    [Range(1,3)]
    public int? SupportYears { get; set; }
    
}