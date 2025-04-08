using Microsoft.EntityFrameworkCore;
using WebStore.Entities;

namespace WebStore
{
    /// Additional tutorial materials https://dotnettutorials.net/lesson/linq-to-entities-in-entity-framework-core/

    /// <summary>
    /// This class demonstrates various LINQ query tasks 
    /// to practice querying an EF Core database.
    /// 
    /// ASSIGNMENT INSTRUCTIONS:
    ///   1. For each method labeled "TODO", write the necessary
    ///      LINQ query to return or display the required data.
    ///      
    ///   2. Print meaningful output to the console (or return
    ///      collections, as needed).
    ///      
    ///   3. Test each method by calling it from your Program.cs
    ///      or test harness.
    /// </summary>
    public class LinqQueriesAssignment
    {
        private readonly WebStoreContext _dbContext;

        public LinqQueriesAssignment(WebStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 1. List all customers in the database:
        ///    - Print each customer's full name (First + Last) and Email.
        /// </summary>
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _dbContext.Customers
                .Include(c => c.Orders)
                .ToList();
        }

        /// <summary>
        /// 2. Fetch all orders along with:
        ///    - Customer Name
        ///    - Order ID
        ///    - Order Status
        ///    - Number of items in each order (the sum of OrderItems.Quantity)
        /// </summary>
        public IEnumerable<Order> GetCustomerOrders(int customerId)
        {
            return _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.CustomerId == customerId)
                .ToList();
        }

        /// <summary>
        /// 3. List all products (ProductName, Price),
        ///    sorted by price descending (highest first).
        /// </summary>
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _dbContext.ProductCategories
                .Include(pc => pc.Product)
                .Where(pc => pc.CategoryId == categoryId)
                .Select(pc => pc.Product)
                .ToList();
        }

        /// <summary>
        /// 4. Find all "Pending" orders (order status = "Pending")
        ///    and display:
        ///      - Customer Name
        ///      - Order ID
        ///      - Order Date
        ///      - Total price (sum of unit_price * quantity - discount) for each order
        /// </summary>
        public IEnumerable<Order> GetOrdersByStatus(string status)
        {
            return _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.OrderStatus == status)
                .ToList();
        }

        /// <summary>
        /// 5. List the total number of orders each customer has placed.
        ///    Output should show:
        ///      - Customer Full Name
        ///      - Number of Orders
        /// </summary>
        public IEnumerable<object> GetStockQuantities()
        {
            return _dbContext.Stocks
                .Include(s => s.Store)
                .Include(s => s.Product)
                .GroupBy(s => s.Store.StoreName)
                .Select(g => new
                {
                    StoreName = g.Key,
                    TotalQuantity = g.Sum(s => s.QuantityInStock)
                })
                .ToList();
        }

        /// <summary>
        /// 6. Show the top 3 customers who have placed the highest total order value overall.
        ///    - For each customer, calculate SUM of (OrderItems * Price).
        ///      Then pick the top 3.
        /// </summary>
        public IEnumerable<Customer> GetCustomersWithOrdersInPeriod(DateTime startDate, DateTime endDate)
        {
            return _dbContext.Orders
                .Include(o => o.Customer)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .Select(o => o.Customer)
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// 7. Show all orders placed in the last 30 days (relative to now).
        ///    - Display order ID, date, and customer name.
        /// </summary>
        public IEnumerable<Product> GetTopExpensiveProducts(int count = 5)
        {
            return _dbContext.Products
                .OrderByDescending(p => p.Price)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// 8. For each product, display how many total items have been sold
        ///    across all orders.
        ///    - Product name, total sold quantity.
        ///    - Sort by total sold descending.
        /// </summary>
        public IEnumerable<object> GetCustomerOrderTotals()
        {
            return _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .GroupBy(o => new { o.CustomerId, o.Customer.FirstName, o.Customer.LastName })
                .Select(g => new
                {
                    CustomerName = $"{g.Key.FirstName} {g.Key.LastName}",
                    TotalAmount = g.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice))
                })
                .OrderByDescending(x => x.TotalAmount)
                .ToList();
        }

        /// <summary>
        /// 9. List any orders that have at least one OrderItem with a Discount > 0.
        ///    - Show Order ID, Customer name, and which products were discounted.
        /// </summary>
        public IEnumerable<Order> GetDiscountedOrders()
        {
            return _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.OrderItems.Any(oi => oi.Discount > 0))
                .ToList();
        }

        /// <summary>
        /// 10. (Open-ended) Combine multiple joins or navigation properties
        ///     to retrieve a more complex set of data. For example:
        ///     - All orders that contain products in a certain category
        ///       (e.g., "Electronics"), including the store where each product
        ///       is stocked most. (Requires `Stocks`, `Store`, `ProductCategory`, etc.)
        ///     - Or any custom scenario that spans multiple tables.
        /// </summary>
        public IEnumerable<object> GetOrdersWithCategoryProducts(string categoryName)
        {
            return _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Where(o => o.OrderItems.Any(oi => 
                    oi.Product.ProductCategories.Any(pc => 
                        pc.Category.CategoryName == categoryName)))
                .Select(o => new
                {
                    OrderId = o.OrderId,
                    CustomerName = $"{o.Customer.FirstName} {o.Customer.LastName}",
                    Products = o.OrderItems
                        .Where(oi => oi.Product.ProductCategories
                            .Any(pc => pc.Category.CategoryName == categoryName))
                        .Select(oi => new
                        {
                            ProductName = oi.Product.ProductName,
                            Quantity = oi.Quantity,
                            Price = oi.UnitPrice
                        })
                })
                .ToList();
        }
    }
}
