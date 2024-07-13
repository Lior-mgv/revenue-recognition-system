using System.ComponentModel.DataAnnotations;

namespace FinancesProject.DTO;

public class IndividualClientPatch
{
    [MaxLength(100)]
    public string? Email { get; set; }
    
    [MaxLength(100)]
    public string? PhoneNumber { get; set; }
    
    [MaxLength(100)]
    public string? FirstName { get; set; }
    
    [MaxLength(100)]
    public string? LastName { get; set; }
    
    [MaxLength(100)]
    public string? Address { get; set; }
}