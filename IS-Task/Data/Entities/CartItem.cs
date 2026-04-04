using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_Task.Data.Entities
{
    public class CartItem
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } = null!;
        public long ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
