using AutoMapper;
using IS_Task.Core.Interfaces;
using IS_Task.Core.Services;
using IS_Task.Data.Entities;
using IS_Task.Interfaces;
using IS_Task.Models;
using IS_Task.Models.RequestModels;
using IS_Task.Services;
using IS_Task.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IS_Task.Controllers
{
    public class ProductController(
        ICategoryService categoryService,
        IProductService productService,
        ICartService cartService) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] GetAllProductsQueryModel model)
        {
            var result = await productService.AllAsync(
                model.CategoryId,
                model.CurrentPage,
                model.ProductsPerPage);

            model.TotalProductsCount = result.TotalProductsCount;
            model.TotalPages = result.TotalPages;
            model.Products = result.Products;
            model.Categories = await categoryService.GetAllCategories();

            await cartService.GetOrCreateCart(AuthenticationHelper.GetCartToken(User, Request, Response), AuthenticationHelper.GetUserId(User));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var product = await productService.GetProductById(id);
            if (product is null)
                return NotFound();

            return View(product);
        }
    }
}
