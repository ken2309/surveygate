using System.Numerics;
using learningGate.Models;

namespace learningGate.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product>? Products { get; set; } 
        public HomeUserCreateViewModel Register { get; set; } = new HomeUserCreateViewModel();
    }
}