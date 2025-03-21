using eCommerce.OrderMicroservice.DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.DataAccessLayer.DbContext1;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Order entity
        modelBuilder.Entity<Order>()
            .HasKey(o => o.OrderID);

        // Configure OrderItem entity
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => oi.OrderItemID);

        // Configure relationship
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderID);

        base.OnModelCreating(modelBuilder);
    }
}
