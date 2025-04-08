using Microsoft.EntityFrameworkCore;
using WebStore.Entities;

namespace WebStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebStoreContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=WebStore;Username=postgres;Password=your_password");

            using (var context = new WebStoreContext(optionsBuilder.Options))
            {
                var queries = new LinqQueriesAssignment(context);

                // 1. Display all customers
                Console.WriteLine("=== All Customers ===");
                var customers = queries.GetAllCustomers();
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.FirstName} {customer.LastName} - {customer.Email}");
                }

                // 2. Orders for customer with ID 1
                Console.WriteLine("\n=== Orders for Customer ID 1 ===");
                var orders = queries.GetCustomerOrders(1);
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order #{order.OrderId} from {order.OrderDate}");
                }

                // 3. Products in category 1
                Console.WriteLine("\n=== Products in Category 1 ===");
                var products = queries.GetProductsByCategory(1);
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.ProductName} - {product.Price:C}");
                }

                // 4. Orders with "Pending" status
                Console.WriteLine("\n=== Orders with 'Pending' Status ===");
                var pendingOrders = queries.GetOrdersByStatus("Pending");
                foreach (var order in pendingOrders)
                {
                    Console.WriteLine($"Order #{order.OrderId} from {order.Customer.FirstName} {order.Customer.LastName}");
                }

                // 5. Stock quantities per store
                Console.WriteLine("\n=== Stock Quantities per Store ===");
                var stockQuantities = queries.GetStockQuantities();
                foreach (var stock in stockQuantities)
                {
                    Console.WriteLine($"{stock.StoreName}: {stock.TotalQuantity} units");
                }

                // 6. Customers with orders in the last month
                Console.WriteLine("\n=== Customers with Orders in the Last Month ===");
                var lastMonthCustomers = queries.GetCustomersWithOrdersInPeriod(
                    DateTime.Now.AddMonths(-1),
                    DateTime.Now
                );
                foreach (var customer in lastMonthCustomers)
                {
                    Console.WriteLine($"{customer.FirstName} {customer.LastName}");
                }

                // 7. Top 5 expensive products
                Console.WriteLine("\n=== Top 5 Expensive Products ===");
                var expensiveProducts = queries.GetTopExpensiveProducts();
                foreach (var product in expensiveProducts)
                {
                    Console.WriteLine($"{product.ProductName} - {product.Price:C}");
                }

                // 8. Total order amounts per customer
                Console.WriteLine("\n=== Total Order Amounts per Customer ===");
                var customerTotals = queries.GetCustomerOrderTotals();
                foreach (var total in customerTotals)
                {
                    Console.WriteLine($"{total.CustomerName}: {total.TotalAmount:C}");
                }

                // 9. Orders with discounts
                Console.WriteLine("\n=== Orders with Discounts ===");
                var discountedOrders = queries.GetDiscountedOrders();
                foreach (var order in discountedOrders)
                {
                    Console.WriteLine($"Order #{order.OrderId} from {order.Customer.FirstName} {order.Customer.LastName}");
                }

                // 10. Complex query
                Console.WriteLine("\n=== Orders with 'Electronics' Category Products ===");
                var categoryOrders = queries.GetOrdersWithCategoryProducts("Electronics");
                foreach (var order in categoryOrders)
                {
                    Console.WriteLine($"Order #{order.OrderId} from {order.CustomerName}");
                    foreach (var product in order.Products)
                    {
                        Console.WriteLine($"  - {product.ProductName}: {product.Quantity} pcs. at {product.Price:C}");
                    }
                }
            }
        }
    }
}
