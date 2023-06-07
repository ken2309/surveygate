using ssSystem.Models;

namespace ssSystem.ViewModels
{
    public class EditProfileViewModel
    {
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public IFormFile? Image { get; set; }
        
        public virtual ICollection<Image>? Images { get; set; } = new List<Image>();
    }
}