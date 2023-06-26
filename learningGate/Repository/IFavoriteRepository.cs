using learningGate.Models;

namespace learningGate.Repository
{
    public interface IFavoriteRepository
    {
        Task<int> AddItem(int bookId, int qty);
        Task<int> RemoveItem(int bookId, Boolean? isRemove);
        Task<FavoriteCart> GetUserFavorite();
        Task<int> GetCartItemCount(string userId = "");
        Task<FavoriteCart> GetCart(string userId);
        Task<bool> DoCheckout();
    }
}
