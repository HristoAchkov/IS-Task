using IS_Task.Models.ResponseModels;
using IS_Task.Shared.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

public class GetAllProductsQueryModel
{
    public int ProductsPerPage { get; } = 6;

    public int? CategoryId { get; set; }

    public int CurrentPage { get; set; } = 1;

    public int TotalProductsCount { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<GetAllProductsResponseModel> Products { get; set; }
        = new List<GetAllProductsResponseModel>();

    public IEnumerable<GetAllCategoriesResponseModel> Categories { get; set; }
        = new List<GetAllCategoriesResponseModel>();
}