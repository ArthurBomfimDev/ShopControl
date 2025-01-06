using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Persistence.Mapping;

namespace ProjetoTeste.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        // Construtor que injeta as opções do DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Propriedades DbSet para facilitar o acesso às entidades
        public DbSet<Customer> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }

        // Configuração do modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica todas as configurações do assembly atual
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);

            base.OnModelCreating(modelBuilder); // Boa prática: chama o método base
        }
    }
}
