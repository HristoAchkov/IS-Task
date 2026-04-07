using IS_Task.Models.ResponseModels;
using IS_Task.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace IS_Task.Areas.Admin.Models.RequestModels
{
    public class CreateProductModel
    {
        [Required]
        [MaxLength(30, ErrorMessage = DataConstants.ProductNameLength)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = DataConstants.ProductDescriptionLength)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = DataConstants.PriceRange)]
        public decimal Price { get; set; }
        [Display(Name = "Category")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Every product must have a Category.")]
        public long CategoryId { get; set; }
        public IEnumerable<GetAllCategoriesResponseModel> Categories { get; set; } = new List<GetAllCategoriesResponseModel>();
    }
}
