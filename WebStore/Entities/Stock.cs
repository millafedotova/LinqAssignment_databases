namespace WebStore.Entities
{
    public class Stock
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Store Store { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
} 