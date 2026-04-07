using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_Task.Data.Entities
{
    public class Cart
    {
        public long Id { get; set; }
        public string? SessionToken { get; set; }
        public long? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityApplicationUser User { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
