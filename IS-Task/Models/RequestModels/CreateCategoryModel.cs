using IS_Task.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace IS_Task.Models.RequestModels
{
    public class CreateCategoryModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = DataConstants.CategoryNameLength)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = DataConstants.CategoryDescriptionLength)]
        public string Description { get; set; } = string.Empty;
    }
}
