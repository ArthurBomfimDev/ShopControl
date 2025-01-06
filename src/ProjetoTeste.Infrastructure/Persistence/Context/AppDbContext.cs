using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Persistence.Mapping;

namespace ProjetoTeste.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        // Construtor que injeta as opções do DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);

            base.OnModelCreating(modelBuilder); // Boa prática: chama o método base
        }
    }
}
