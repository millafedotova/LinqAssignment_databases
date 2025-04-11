using System.Collections.Generic;

namespace WebStore.Entities
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        public ICollection<Staff> Staff { get; set; } = new List<Staff>();
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
} 