using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace learningGate.Models
{
    [Table("FavoriteCart")]
    public class FavoriteCart
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<FavoriteDetail> FavoriteDetails { get; set; }
        
    }
}
