using System.ComponentModel.DataAnnotations;

namespace FinancesProject.Models;

public class Product
{
    [Key]
    public int IdProduct { get; set; }

    [MaxLength(100)] 
    public string Name { get; set; } = null!;

    [MaxLength(100)] 
    public string Description { get; set; } = null!;

    public decimal Price { get; set; }
    
    public IEnumerable<ProductVersion> Versions { get; set; } = new List<ProductVersion>();

    [MaxLength(100)]
    public string Category { get; set; } = null!;

    public virtual IEnumerable<Discount> Discounts { get; set; } = new List<Discount>();
}