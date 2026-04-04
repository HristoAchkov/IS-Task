using AutoMapper;
using IS_Task.Interfaces;
using IS_Task.Mappings;
using IS_Task.Models;
using IS_Task.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace IS_Task.Controllers
{
    public class CategoryController(
        ICategoryService categoryService,
        IMapper mapper) : BaseAdminController
    {
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            var allCategories = await categoryService.GetAllCategories(); 
            return View("GetAllCategories", allCategories);
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category = await categoryService.GetCategoryById(id);

            if (category is null)
                return View("Error", new ErrorViewModel());
            else
                return View("Details", category);
        }

        // GET: CategoryController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var result = await categoryService.CreateCategory(categoryModel);
                return View("CategorySuccess");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryService.GetCategoryById(id);

            if (category == null)
                return NotFound();

            var editCategoryModel = mapper.Map<EditCategoryModel>(category);

            return View(editCategoryModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await categoryService.UpdateCategory(model);

            return RedirectToAction("Index");
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await categoryService.DeleteCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }
    }
}
