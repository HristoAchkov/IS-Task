using AutoMapper;
using IS_Task.Areas.Admin.Models.RequestModels;
using IS_Task.Areas.Admin.Models.ResponseModels;
using IS_Task.Data;
using IS_Task.Data.Entities;
using IS_Task.Interfaces;
using IS_Task.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IS_Task.Services
{
    public class CategoryService(
        ApplicationDbContext dbContext,
        IMapper mapper) : ICategoryService
    {
        public async Task CreateCategory(CreateCategoryModel categoryModel)
        {
            var category = mapper.Map<Category>(categoryModel);

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<GetAllCategoriesResponseModel>> GetAllCategories()
        {
            return await dbContext.Categories
                .Select(x => new GetAllCategoriesResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Status = x.Status
                })
                .ToListAsync();
        }

        public async Task<GetCategoryByIdResponseModel> GetCategoryById(long id)
        {
            return await dbContext.Categories
                .Select(x => new GetCategoryByIdResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Status = x.Status
                })
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateCategory(EditCategoryModel editCategoryModel)
        {
            var existingCategory = await dbContext.Categories
                .FindAsync(editCategoryModel.Id);

            if (existingCategory is not null)
            {
                mapper.Map(editCategoryModel, existingCategory);
                existingCategory.UpdatedAt = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteCategory(long id)
        {
            await dbContext.Categories
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task SoftDeleteCategory(long id)
        {
            var category = await dbContext.Categories
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (category is not null)
            {
                category.Status = Shared.Enums.CategoryStatus.Archived;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
