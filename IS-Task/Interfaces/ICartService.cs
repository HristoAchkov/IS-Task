using IS_Task.Data.Entities;
using IS_Task.Models.ResponseModels;

namespace IS_Task.Interfaces
{
    public interface ICartService
    {
        Task<GetCartResponseModel> GetOrCreateCart(string token, long? userId);
        Task<GetCartResponseModel> AddCartItemToCart(long productId, string token, long? userId);
        Task<GetCartResponseModel> RemoveCartItemFromCart(long cartItemId, string token, long? userId);
        Task ClearCart(string token, long? userId);
    }
}
