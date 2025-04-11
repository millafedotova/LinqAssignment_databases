using System.Collections.Generic;

namespace WebStore.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
} 