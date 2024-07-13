using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancesProject.Models;

public class ProductVersion
{
    [Key]
    public int IdProductVersion { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;
    
    public int IdProduct { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual IEnumerable<Contract> Contracts { get; set; } = new List<Contract>();
}