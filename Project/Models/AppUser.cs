using System.ComponentModel.DataAnnotations;

namespace FinancesProject.Models;

public class AppUser
{
    [Key] 
    public int IdAppUser { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;
    
    public string? RefreshToken { get; set; }
    
    public DateTime? RefreshTokenExp { get; set; }

    public string Role { get; set; } = null!;
}