using IS_Task.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_Task.Data.Entities
{
    public class Order
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityApplicationUser User { get; set; } = null!;
        public OrderStatus Status { get; set; }
        [Precision(18,2)]
        public decimal TotalAmount { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string FamilyName { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
