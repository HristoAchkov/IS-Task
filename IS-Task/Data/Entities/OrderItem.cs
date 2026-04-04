using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_Task.Data.Entities
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;
        public long ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        [Precision(18,2)]
        public decimal UnitPrice { get; set; }
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }
    }
}
