using IS_Task.Interfaces;
using IS_Task.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace IS_Task.Controllers
{
    public class OrderController(
        IOrderService orderService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetOrderHistory()
        {
            var userId = AuthenticationHelper.GetUserId(User);

            var orders = await orderService.ListAllOrders(userId);

            return View(orders);
        }
    }
}
