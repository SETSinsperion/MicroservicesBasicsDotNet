using Microsoft.EntityFrameworkCore;
using OrderMS.Models;

namespace OrderMS.Data {
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { set; get; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}