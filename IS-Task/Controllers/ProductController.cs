using IS_Task.Core.Interfaces;
using IS_Task.Core.Services;
using IS_Task.Interfaces;
using IS_Task.Models;
using IS_Task.Models.RequestModels;
using IS_Task.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IS_Task.Controllers
{
    public class ProductController(ICategoryService categoryService,
        IProductService productService) : Controller
    {
        // GET: ProductController
        public async Task<IActionResult> Index()
        {
            var allProducts = await productService.GetAllProducts();
            return View("GetAllProducts", allProducts);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(
                await categoryService.GetAllCategories(), "Id", "Name"
            );

            return View("Create");
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductModel productModel)
        { 
            if (!ModelState.IsValid)
            {
                return View("Error", new ErrorViewModel());
            }

            await productService.CreateProduct(productModel);
            return View("ProductSuccess");
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
