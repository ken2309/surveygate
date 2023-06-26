using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using learningGate.Models;

namespace learningGate.Models
{
    [Table("FavoriteDetail")]
    public class FavoriteDetail
    {
        public int Id { get; set; }
        [Required] public int FavoriteCartId { get; set; }
        [Required] public int ProductId { get; set; }
        [Required] public int? Quantity { get; set; }
        [Required] public decimal? UnitPrice { get; set; }
        public Product Product { get; set; }

        public FavoriteCart FavoriteCart { get; set; }
    }
}