using IS_Task.Data;
using IS_Task.Data.Entities;
using IS_Task.Interfaces;
using IS_Task.Models.RequestModels;
using IS_Task.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace IS_Task.Services
{
    public class OrderService(ApplicationDbContext dbContext) : IOrderService
    {
        public async Task CreateOrder(PostCheckoutModel checkoutModel, GetCartResponseModel cartModel, long? userId)
        {
            if (cartModel is null)
            {
                throw new ArgumentNullException(nameof(cartModel));
            }

            var order = new Order()
            {
                Address = checkoutModel.Address,
                Email = checkoutModel.Email,
                FamilyName = checkoutModel.FamilyName,
                Name = checkoutModel.Name,
                Phone = checkoutModel.Phone,
                UserId = userId,
                Status = Shared.Enums.OrderStatus.Pending,
                TotalAmount = cartModel.CartItems.Sum(x => x.LineTotal),
                OrderItems = cartModel.CartItems.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.Price
                }).ToList()
            };

            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
        }
        
        public async Task<List<GetAllOrdersResponseModel>> ListAllOrders(long? userId)
        {
            var orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.UserId == userId)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

            if (orders == null)
                return new List<GetAllOrdersResponseModel>();

            return orders.Select(x => new GetAllOrdersResponseModel
            {
                Id = x.Id,
                Name = x.Name,
                FamilyName = x.FamilyName,
                Address = x.Address,
                Phone = x.Phone,
                Email = x.Email,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                OrderItems = x.OrderItems.Select(o => new OrderItemResponseModel
                {
                    ProductName = o.Product.Name,
                    Price = o.UnitPrice,
                    Quantity = o.Quantity
                }).ToList()
            }).ToList();
        }
    }
}
