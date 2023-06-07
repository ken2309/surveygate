using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using learningGate.Models;

namespace learningGate.Models;

public partial class Product
{
    [Key] public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
    
    public string? ShortDescription { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public int ProductTypeId { get; set; }
    
    public bool IsSerivce { get; set; }

    public bool Status { get; set; }

    public virtual List<Image>? Images { get; set; } = new List<Image>();

    public virtual Producttype? ProductType { get; set; }
}