using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace learningGate.Models;

public partial class Image
{
    [Key] public int Id { get; set; }
    
    public string? ImageCloud { get; set; }
    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
}