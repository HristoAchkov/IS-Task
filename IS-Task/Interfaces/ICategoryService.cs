using IS_Task.Areas.Admin.Models.RequestModels;
using IS_Task.Areas.Admin.Models.ResponseModels;
using IS_Task.Models.ResponseModels;

namespace IS_Task.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategory(CreateCategoryModel categoryModel);
        Task<List<GetAllCategoriesResponseModel>> GetAllCategories();
        Task<GetCategoryByIdResponseModel> GetCategoryById(long id);
        Task UpdateCategory(EditCategoryModel editCategory);
        Task DeleteCategory(long id);
    }
}
