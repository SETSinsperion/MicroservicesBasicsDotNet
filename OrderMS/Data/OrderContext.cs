using Microsoft.EntityFrameworkCore;
using OrderMS.Models;

namespace OrderMS.Data {
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { set; get; }

        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(x => x.Total)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);            
        }
    }
}