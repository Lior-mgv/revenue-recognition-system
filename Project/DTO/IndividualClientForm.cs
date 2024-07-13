using System.ComponentModel.DataAnnotations;

namespace FinancesProject.DTO;

public class IndividualClientForm
{
    [MaxLength(100)]
    [Required]
    public required string Email { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string PhoneNumber { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string FirstName { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string LastName { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string Address { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string Pesel { get; set; }
}