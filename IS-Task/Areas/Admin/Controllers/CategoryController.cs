using AutoMapper;
using IS_Task.Areas.Admin.Models.RequestModels;
using IS_Task.Interfaces;
using IS_Task.Mappings;
using IS_Task.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IS_Task.Areas.Admin.Controllers
{
    public class CategoryController(
        ICategoryService categoryService,
        IMapper mapper) : BaseAdminController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var allCategories = await categoryService.GetAllCategories(); 
            return View(allCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await categoryService.GetCategoryById(id);

            if (category is null)
                return NotFound();
            
            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
                return View(categoryModel);

            await categoryService.CreateCategory(categoryModel);
            return RedirectToAction(nameof(CategorySuccess));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryService.GetCategoryById(id);

            if (category is null)
                return NotFound();

            var editCategoryModel = mapper.Map<EditCategoryModel>(category);

            return View(editCategoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCategoryModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await categoryService.UpdateCategory(model);
            return RedirectToAction(nameof(GetAllCategories));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(GetAllCategories));
        }

        [HttpGet]
        public IActionResult CategorySuccess()
        {
            return View();
        }
    }
}
