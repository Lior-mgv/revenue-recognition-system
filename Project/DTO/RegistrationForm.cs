using System.ComponentModel.DataAnnotations;

namespace FinancesProject.DTO;

public class RegistrationForm
{
    [Required]
    public required string Login { get; set; }
    
    [Required]
    public string Password { get; set; }
}