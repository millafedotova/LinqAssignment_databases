using Microsoft.EntityFrameworkCore;
using WebStore.Entities;

namespace WebStore
{
    public class WebStoreContext : DbContext
    {
        public WebStoreContext(DbContextOptions<WebStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<Store> Stores => Set<Store>();
        public DbSet<Staff> Staff => Set<Staff>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<Carrier> Carriers => Set<Carrier>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("customers_pkey");
                entity.ToTable("customers");
                entity.Property(e => e.FirstName).HasMaxLength(50).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasMaxLength(50).HasColumnName("last_name");
                entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
                entity.Property(e => e.Phone).HasMaxLength(20).HasColumnName("phone");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            // Order configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("orders_pkey");
                entity.ToTable("orders");
                entity.Property(e => e.OrderDate).HasColumnName("order_date");
                entity.Property(e => e.OrderStatus).HasMaxLength(20).HasColumnName("order_status");
                entity.Property(e => e.ShippingAddressId).HasColumnName("shipping_address_id");
                entity.Property(e => e.BillingAddressId).HasColumnName("billing_address_id");
                entity.Property(e => e.CarrierId).HasColumnName("carrier_id");
                entity.Property(e => e.TrackingNumber).HasMaxLength(50).HasColumnName("tracking_number");
                entity.Property(e => e.ShippedDate).HasColumnName("shipped_date");
                entity.Property(e => e.DeliveredDate).HasColumnName("delivered_date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_orders_customer");

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CarrierId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_orders_carrier");
            });

            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("products_pkey");
                entity.ToTable("products");
                entity.Property(e => e.ProductName).HasMaxLength(100).HasColumnName("product_name");
                entity.Property(e => e.Description).HasMaxLength(255).HasColumnName("description");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            // Category configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("categories_pkey");
                entity.ToTable("categories");
                entity.Property(e => e.CategoryName).HasMaxLength(100).HasColumnName("category_name");
                entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.ChildCategories)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_categories_parent");
            });

            // OrderItem configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("pk_order_items");
                entity.ToTable("order_items");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_oi_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_oi_product");
            });

            // ProductCategory configuration
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.ProductId }).HasName("pk_product_categories");
                entity.ToTable("product_categories");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_pc_category");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_pc_product");
            });

            // Store configuration
            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId).HasName("stores_pkey");
                entity.ToTable("stores");
                entity.Property(e => e.StoreName).HasMaxLength(100).HasColumnName("store_name");
                entity.Property(e => e.Phone).HasMaxLength(20).HasColumnName("phone");
                entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
                entity.Property(e => e.Street).HasMaxLength(100).HasColumnName("street");
                entity.Property(e => e.City).HasMaxLength(50).HasColumnName("city");
                entity.Property(e => e.PostalCode).HasMaxLength(20).HasColumnName("postal_code");
                entity.Property(e => e.Country).HasMaxLength(50).HasColumnName("country");
            });

            // Staff configuration
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.StaffId).HasName("staff_pkey");
                entity.ToTable("staff");
                entity.Property(e => e.FirstName).HasMaxLength(50).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasMaxLength(50).HasColumnName("last_name");
                entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
                entity.Property(e => e.Phone).HasMaxLength(20).HasColumnName("phone");
                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_staff_store");
            });

            // Stock configuration
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ProductId }).HasName("pk_stocks");
                entity.ToTable("stocks");
                entity.Property(e => e.QuantityInStock).HasColumnName("quantity_in_stock");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_stocks_store");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_stocks_product");
            });

            // Carrier configuration
            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.HasKey(e => e.CarrierId).HasName("carriers_pkey");
                entity.ToTable("carriers");
                entity.Property(e => e.CarrierName)
                    .HasMaxLength(50)
                    .HasColumnName("carrier_name");
                entity.Property(e => e.ContactUrl)
                    .HasMaxLength(50)
                    .HasColumnName("contact_url");
                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(50)
                    .HasColumnName("contact_phone");
            });
        }
    }
} 