using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class BrandRepository : Repository<Brand>, IBrandRepository
{
    public BrandRepository(AppDbContext context) : base(context)
    {
    }

    public bool BrandExists(long id)
    {
        return _dbSet.Any(x => x.Id == id);
    }

    public async Task<List<Brand>> GetListByListCode(List<string> listCode)
    {
        return await _dbSet.Where(i => listCode.Contains(i.Code)).ToListAsync();
    }

    public async Task<Brand> GetByCode(string code)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<List<Brand>> GetAllAndProduct()
    {
        return await _context.Brand.Include(i => i.ListProduct).ToListAsync();
    }

    public async Task<List<Brand>> GetAndProduct(long id)
    {
        return await _context.Brand.Include(i => i.ListProduct).Where(i => i.Id == id).ToListAsync();
    }
}