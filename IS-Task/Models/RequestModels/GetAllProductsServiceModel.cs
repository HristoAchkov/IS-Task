using IS_Task.Models.ResponseModels;

namespace IS_Task.Models.RequestModels
{
    public class GetAllProductsServiceModel
    {
        public int TotalProductsCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<GetAllProductsResponseModel> Products { get; set; } = new List<GetAllProductsResponseModel>();
    }
}
