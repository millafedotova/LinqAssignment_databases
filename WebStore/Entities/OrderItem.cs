namespace WebStore.Entities
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Discount { get; set; }

        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
} 