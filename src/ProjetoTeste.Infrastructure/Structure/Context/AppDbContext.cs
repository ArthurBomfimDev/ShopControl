using Microsoft.EntityFrameworkCore;
using ProjetoEstagioAPI.Configurations;
using ProjetoEstagioAPI.Models;

namespace ProjetoEstagioAPI.Context
{
    public class AppDbContext : DbContext
    {
        // Construtor que injeta as opções do DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Propriedades DbSet para facilitar o acesso às entidades
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }

        // Configuração do modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica todas as configurações do assembly atual
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientConfiguration).Assembly);

            base.OnModelCreating(modelBuilder); // Boa prática: chama o método base
        }
    }
}
