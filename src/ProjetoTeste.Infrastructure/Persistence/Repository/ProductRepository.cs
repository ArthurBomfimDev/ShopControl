using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity;

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

        public Task<bool> ExistUpdate(string code, long id)
        {
            throw new NotImplementedException();
        }
    }
}