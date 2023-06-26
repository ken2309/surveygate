using learningGate.Models;

namespace learningGate.ViewModels;

public class ProductViewModel
{
    public int ProductId { get; set; }

    // Other product properties
    public string? Name { get; set; }
    public string? AuthorName { get; set; }

    public string? Description { get; set; }

    public string? ShortDescription { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public int ProductTypeId { get; set; }

    public string? ImageCloud { get; set; }
    public int? ImageId { get; set; }

    public bool IsSerivce { get; set; }

    public bool Status { get; set; }

    public List<Product>? relatedProducts { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Producttype? ProductType { get; set; }
    public List<Image> Images { get; set; }
}

public class ImageViewModel
{
    public int ImageId { get; set; }

    public string FileName { get; set; }
    // Other image properties
}