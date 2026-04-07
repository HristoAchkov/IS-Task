using IS_Task.Shared.Constants;
using IS_Task.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace IS_Task.Models.ResponseModels
{
    public class GetAllProductsResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public ProductStatus Status { get; set; }
    }
}
