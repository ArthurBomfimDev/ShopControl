using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Context;

namespace ProjetoTeste.Infrastructure.Persistence.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
