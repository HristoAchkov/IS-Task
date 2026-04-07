using AspNetCoreGeneratedDocument;
using AutoMapper;
using IS_Task.Areas.Admin.Models.RequestModels;
using IS_Task.Core.Interfaces;
using IS_Task.Interfaces;
using IS_Task.Models;
using Microsoft.AspNetCore.Mvc;

namespace IS_Task.Areas.Admin.Controllers
{
    public class ProductController(
        IProductService productService, 
        ICategoryService categoryService,
        IMapper mapper) : BaseAdminController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAdmin()
        {
            var products = await productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateProductModel
            {
                Categories = await categoryService.GetAllCategories()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                productModel.Categories = await categoryService.GetAllCategories();

                return View(productModel);
            }

            await productService.CreateProduct(productModel);

            return RedirectToAction(nameof(ProductSuccess));
        }

        [HttpGet]
        public IActionResult ProductSuccess()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productService.GetProductById(id);

            if (product is null)
                return NotFound();

            var editProductModel = mapper.Map<EditProductModel>(product);

            editProductModel.Categories = await categoryService.GetAllCategories();

            return View(editProductModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategories();

                return View(model);
            }

            await productService.UpdateProduct(model);

            return RedirectToAction(nameof(GetAllProductsAdmin));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteProduct(id);

            return RedirectToAction(nameof(GetAllProductsAdmin));
        }
    }
}
