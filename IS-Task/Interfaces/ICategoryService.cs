using IS_Task.Models.RequestModels;
using IS_Task.Models.ResponseModels;

namespace IS_Task.Interfaces
{
    public interface ICategoryService
    {
        Task<int> CreateCategory(CreateCategoryModel categoryModel);
        Task<List<GetAllCategoriesResponseModel>> GetAllCategories();
        Task<GetCategoryByIdResponseModel> GetCategoryById(long id);
        Task UpdateCategory(EditCategoryModel editCategory);
        Task DeleteCategory(long id);
    }
}
