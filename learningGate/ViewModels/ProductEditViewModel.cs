using learningGate.Models;

namespace learningGate.ViewModels;

public class ProductEditViewModel
{
    public int Id { get; set; }

    public List<IFormFile>? Image { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
    
    public string? ShortDescription { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public int ProductTypeId { get; set; }

    public bool IsSerivce { get; set; }

    public bool Status { get; set; }

    public List<Image>? Images { get; set; } = new List<Image>();

    // public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    // public virtual ICollection<Invoicedetail> Invoicedetails { get; set; } = new List<Invoicedetail>();

    public virtual Producttype? ProductType { get; set; }
}