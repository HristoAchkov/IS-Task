using AutoMapper;
using IS_Task.Data;
using IS_Task.Data.Entities;
using IS_Task.Interfaces;
using IS_Task.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace IS_Task.Services
{
    public class CartService(
        ApplicationDbContext dbContext,
        IMapper mapper) : ICartService
    {
        public async Task<GetCartResponseModel> GetOrCreateCart(string token, long? userId)
        {
            var cart = userId.HasValue
                ? await GetCartWithItems().FirstOrDefaultAsync(x => x.UserId == userId)
                : await GetCartWithItems().FirstOrDefaultAsync(x => x.SessionToken == token);

            if (cart is null)
            {
                cart = new Cart { SessionToken = token, UserId = userId };
                await dbContext.Carts.AddAsync(cart);
                await dbContext.SaveChangesAsync();
            }

            return mapper.Map<GetCartResponseModel>(cart);
        }

        public async Task<GetCartResponseModel> AddCartItemToCart(long productId, string token, long? userId)
        {
            var cart = userId.HasValue
                ? await GetCartWithItems().FirstOrDefaultAsync(x => x.UserId == userId)
                : await GetCartWithItems().FirstOrDefaultAsync(x => x.SessionToken == token);

            var product = await dbContext.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == productId);

            var existing = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);
            if (existing != null)
                existing.Quantity++;
            else
            {
                var cartItem = new CartItem { ProductId = productId, Quantity = 1 };
                cartItem.Product = product;
                cart.CartItems.Add(cartItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();

            return mapper.Map<GetCartResponseModel>(cart);
        }

        public async Task<GetCartResponseModel> RemoveCartItemFromCart(long cartItemId, string token, long? userId)
        {
            var cart = userId.HasValue
                ? await GetCartWithItems().FirstOrDefaultAsync(x => x.UserId == userId)
                : await GetCartWithItems().FirstOrDefaultAsync(x => x.SessionToken == token);

            var item = cart.CartItems.FirstOrDefault(x => x.Id == cartItemId);
            if (item is not null)
            {
                if (item.Quantity > 1)
                    item.Quantity--;
                else
                    cart.CartItems.Remove(item);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();

            return mapper.Map<GetCartResponseModel>(cart);
        }
        public async Task ClearCart(string token, long? userId)
        {
            var cart = userId.HasValue
                ? await GetCartWithItems().FirstOrDefaultAsync(x => x.UserId == userId)
                : await GetCartWithItems().FirstOrDefaultAsync(x => x.SessionToken == token);

            if (cart == null) return;

            await dbContext.CartItems
                .Where(x => x.CartId == cart.Id)
                .ExecuteDeleteAsync();
        }

        private IQueryable<Cart> GetCartWithItems()
        {
            return dbContext.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category);
        }
    }
}
