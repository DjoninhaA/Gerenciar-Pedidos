using gerenciar_pedidos.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}


    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderDetails> OrderDetails { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasMany(order => order.OrderDetails)
            .WithOne(OrderDetails => OrderDetails.Order)
            .HasForeignKey(OrderDetails => OrderDetails.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasMany(product => product.OrderDetails)
            .WithOne(orderDetails => orderDetails.Product)
            .HasForeignKey(orderDetails => orderDetails.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderDetails>()
            .HasKey(orderDetails => new {orderDetails.OrderId, orderDetails.ProductId });
    }

}