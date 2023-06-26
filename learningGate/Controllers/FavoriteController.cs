using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using learningGate.Repository;

namespace learningGate.Controllers
{
    // [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _cartRepo;

        public FavoriteController(IFavoriteRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IActionResult> AddItem(int productId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(productId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserFavorite");
        }

        public async Task<IActionResult> RemoveItem(int bookId, Boolean? isRemove)
        {
            var cartCount = await _cartRepo.RemoveItem(bookId,isRemove);
            return RedirectToAction("GetUserFavorite");
        }
        public async Task<IActionResult> GetUserFavorite()
        {
            var cart = await _cartRepo.GetUserFavorite();
            return View(cart);
        }

        public  async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }

        public async Task<IActionResult> Checkout()
        {
            bool isCheckedOut = await _cartRepo.DoCheckout();
            if (!isCheckedOut)
                throw new Exception("Something happen in server side");
            return RedirectToAction("Index", "Home");
        }

    }
}
