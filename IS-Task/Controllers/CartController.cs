using IS_Task.Data.Entities;
using IS_Task.Interfaces;
using IS_Task.Models.RequestModels;
using IS_Task.Services;
using IS_Task.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IS_Task.Controllers
{
    public class CartController(
        ICartService cartService,
        IOrderService orderService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cart = await cartService.GetOrCreateCart(
                AuthenticationHelper.GetCartToken(User, Request, Response),
                AuthenticationHelper.GetUserId(User));
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(long productId)
        {
            await cartService.AddCartItemToCart(
                productId,
                AuthenticationHelper.GetCartToken(User, Request, Response),
                AuthenticationHelper.GetUserId(User));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(long cartItemId)
        {
            await cartService.RemoveCartItemFromCart(
                cartItemId,
                AuthenticationHelper.GetCartToken(User, Request, Response),
                AuthenticationHelper.GetUserId(User));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var model = new PostCheckoutModel
            {
                Email = User.Identity.IsAuthenticated
                    ? User.FindFirstValue(ClaimTypes.Email)
                    : null
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(PostCheckoutModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = AuthenticationHelper.GetCartToken(User, Request, Response);
            var userId = AuthenticationHelper.GetUserId(User);

            var cart = await cartService.GetOrCreateCart(token, userId);
            await orderService.CreateOrder(model, cart, userId);
            await cartService.ClearCart(token, userId);

            return RedirectToAction(nameof(OrderSuccess));
        }

        [HttpGet]
        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
