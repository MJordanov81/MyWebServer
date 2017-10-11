namespace App.Context
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.\SQLEXPRESS; Database=ByTheCakeDb; Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductOrder>()
                .HasKey(po => new {po.ProductId, po.OrderId});

            builder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(po => po.Product)
                .HasForeignKey(po => po.ProductId);

            builder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne(po => po.Order)
                .HasForeignKey(po => po.OrderId);

            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);
        }
    }
}
