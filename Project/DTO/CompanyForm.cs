using System.ComponentModel.DataAnnotations;

namespace FinancesProject.DTO;

public class CompanyForm
{
    [MaxLength(100)] 
    [Required] 
    public required string Email { get; set; }

    [MaxLength(100)]
    [Required]
    public required string PhoneNumber { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string Name { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string Address { get; set; }
    
    [MaxLength(100)]
    [Required]
    public required string Krs { get; set; }
}