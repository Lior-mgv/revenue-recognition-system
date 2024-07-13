using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancesProject.Models;

public class Contract
{
    [Key] 
    public int IdContract { get; set; }

    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    public decimal Price { get; set; }

    [Range(1,3)]
    public int SupportYears { get; set; }
    
    [ForeignKey("Client")]
    public int IdClient { get; set; }

    public virtual Client Client { get; set; } = null!;

    [ForeignKey("Version")]
    public int IdProductVersion { get; set; }

    public virtual ProductVersion Version { get; set; } = null!;

    public bool IsPaid { get; set; }
    
}