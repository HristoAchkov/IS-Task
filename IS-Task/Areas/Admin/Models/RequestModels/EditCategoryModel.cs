using IS_Task.Shared.Constants;
using IS_Task.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace IS_Task.Areas.Admin.Models.RequestModels
{
    public class EditCategoryModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = DataConstants.CategoryNameLength)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = DataConstants.CategoryDescriptionLength)]
        public string Description { get; set; } = string.Empty;
        public CategoryStatus Status { get; set; }
    }
}
