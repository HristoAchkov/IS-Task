using IS_Task.Data.Entities;
using IS_Task.Models.RequestModels;
using IS_Task.Models.ResponseModels;

namespace IS_Task.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(PostCheckoutModel checkoutModel, GetCartResponseModel cartModel, long? userId);
        Task<List<GetAllOrdersResponseModel>> ListAllOrders(long? userId);
    }
}
