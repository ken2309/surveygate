
using System.ComponentModel.DataAnnotations;

namespace ssSystem.Models;

public partial class Producttype
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Descrition { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
