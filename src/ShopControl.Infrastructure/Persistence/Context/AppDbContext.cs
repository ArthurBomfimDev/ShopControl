using Microsoft.EntityFrameworkCore;
using ShopControl.Infrastructure.Persistence.Entity;

namespace ShopControl.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        // Construtor que injeta as opções do DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Client { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);
        }
    }
}