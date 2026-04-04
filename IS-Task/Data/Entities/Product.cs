using IS_Task.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_Task.Data.Entities
{
    public class Product
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        [Precision(18, 2)]
        public decimal Price { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
        public long CategoryId { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
    }
}
