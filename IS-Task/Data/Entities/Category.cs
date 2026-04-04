using IS_Task.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace IS_Task.Data.Entities
{
    public class Category
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        public CategoryStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
