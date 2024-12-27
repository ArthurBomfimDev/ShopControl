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
        public async Task<bool> Exist(string code)
        {
            return await _dbSet.AnyAsync(x => x.Code == code);
        }
    }
}
