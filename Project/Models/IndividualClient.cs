using System.ComponentModel.DataAnnotations;

namespace FinancesProject.Models;

public class IndividualClient
{
    [Key]
    public int IdIndividualClient { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    
    [MaxLength(100)]
    public string Pesel { get; set; } = null!;
    
    public virtual Client Client { get; set; } = null!;
    
    public int ClientId { get; set; }

    public bool IsDeleted { get; set; }
}