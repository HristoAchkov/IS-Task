using System.ComponentModel.DataAnnotations;

namespace IS_Task.Models.RequestModels
{
    public class PostCheckoutModel
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Family Name")]
        [MaxLength(20)]
        public string FamilyName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }
    }
}
