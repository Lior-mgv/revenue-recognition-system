using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancesProject.Models;

public class Transaction
{
    [Key]
    public int IdTransaction { get; set; }

    [ForeignKey("Contract")]
    public int IdContract { get; set; }

    public virtual Contract Contract { get; set; } = null!;

    public decimal Sum { get; set; }

    public DateTime DateTime { get; set; }
}