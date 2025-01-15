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
        public  async Task<List<Product>> GetListByCodeList(List<string> listCode)
        {
            return await _dbSet.Where(i => listCode.Contains(i.Code)).ToListAsync();
        }

        public Task<bool> ExistUpdate(string code, long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<long>> BrandId(List<long> ids)
        {
            return await _context.Product.Where(i => ids.Contains(i.BrandId)).Select(i => i.BrandId).ToListAsync();
        }

        public bool ProductExists(long id)
        {
            return _dbSet.Any(x => x.Id == id);
        }

        public async Task<List<Product>> GetListByBrandId(long id)
        {
            return await _dbSet.Where(i => i.BrandId == id).ToListAsync();
        }
    }
}