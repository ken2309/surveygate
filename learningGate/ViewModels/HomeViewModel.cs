using System.Numerics;
using ssSystem.Models;

namespace ssSystem.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product>? Products { get; set; } 
        public HomeUserCreateViewModel Register { get; set; } = new HomeUserCreateViewModel();
    }
}