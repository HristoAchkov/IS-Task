using IS_Task.Shared.Enums;

namespace IS_Task.Models.ResponseModels
{
    public class GetAllOrdersResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FamilyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponseModel> OrderItems { get; set; } = new List<OrderItemResponseModel>();
    }
    public class OrderItemResponseModel
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal => Price * Quantity;
    }
}
