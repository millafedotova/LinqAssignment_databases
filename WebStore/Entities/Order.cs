using System;
using System.Collections.Generic;

namespace WebStore.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        public int ShippingAddressId { get; set; }
        public int BillingAddressId { get; set; }
        public int? CarrierId { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        /// <summary>
        /// Navigation to the carrier (e.g. "UPS", "FedEx")
        /// </summary>
        public Carrier? Carrier { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
} 