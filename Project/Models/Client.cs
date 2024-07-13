using System.ComponentModel.DataAnnotations;

namespace FinancesProject.Models;

public class Client
{
    [Key]
    public int IdClient { get; set; }
    
    [MaxLength(100)]
    public string Email { get; set; } = null!;
    
    [MaxLength(100)]
    public string PhoneNumber { get; set; } = null!;
    
    [MaxLength(100)]
    public string Address { get; set; } = null!;

    public virtual IEnumerable<Contract> Contracts { get; set; } = new List<Contract>();
}