using System.ComponentModel.DataAnnotations;

namespace FinancesProject.DTO;

public class CompanyPatch
{
    [MaxLength(100)]
    public string? Email { get; set; }
    
    [MaxLength(100)]
    public string? PhoneNumber { get; set; }
    
    [MaxLength(100)]
    public string? Name { get; set; }
    
    [MaxLength(100)]
    public string? Address { get; set; }
}