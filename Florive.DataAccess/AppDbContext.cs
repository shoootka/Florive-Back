using Florive.Domains.Entities;
using Florive.Domains.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Florive.DataAccess
{
    public class AppDbContext : DbContext
    {
        // Старые
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<SubscriptionOrder> SubscriptionOrders { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Новые
        public DbSet<ProductData> ProductData { get; set; }
        public DbSet<CategoryData> Categories { get; set; }
        public DbSet<ProductImgData> ProductImgs { get; set; }
        public DbSet<ProductDescriptionData> ProductDescriptions { get; set; }
        public DbSet<DescriptionAdvanced> DescriptionAdvanced { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Старые
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SubscriptionPlan>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasPrecision(18, 2);

            // Новые связи
            modelBuilder.Entity<ProductData>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductData>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductData>()
                .HasOne(p => p.Description)
                .WithOne(d => d.Product)
                .HasForeignKey<ProductDescriptionData>(d => d.ProductId);

            modelBuilder.Entity<ProductDescriptionData>()
                .HasOne(d => d.DescriptionAdvanced)
                .WithOne(a => a.Description)
                .HasForeignKey<DescriptionAdvanced>(a => a.DescriptionId);
        }
    }
}