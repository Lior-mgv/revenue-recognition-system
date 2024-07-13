using System.ComponentModel.DataAnnotations;

namespace FinancesProject.Models;

public class Discount
{
    [Key]
    public int IdDiscount { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Range(1,100)]
    public int Percentage { get; set; }

    public DateTime DateFrom { get; set; }
    
    public DateTime DateTo { get; set; }

    public bool ForSubscription { get; set; }

    public bool ForUpfront { get; set; }

    public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();
}