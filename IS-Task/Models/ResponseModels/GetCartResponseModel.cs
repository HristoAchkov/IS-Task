namespace IS_Task.Models.ResponseModels
{
    public class GetCartResponseModel
    {
        public List<CartItemResponseModel> CartItems { get; set; } = new List<CartItemResponseModel>();
        public decimal Total { get; set; }
    }

    public class CartItemResponseModel
    {
        public long ProductId { get; set; }
        public long CartItemId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal => Price * Quantity;
    }
}