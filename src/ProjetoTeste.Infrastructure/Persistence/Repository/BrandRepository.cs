using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Domain.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class BrandRepository : BaseRepository<Brand, BrandDTO>, IBrandRepository
{
    public BrandRepository(AppDbContext context) : base(context)
    {
    }

    public bool BrandExists(long id)
    {
        return _dbSet.Any(x => x.Id == id);
    }

    public async Task<List<BrandDTO>> GetListByListCode(List<string> listCode)
    {
        var getListByListCode = await _dbSet.Where(i => listCode.Contains(i.Code)).ToListAsync();
        return getListByListCode.Select(i => (BrandDTO)i).ToList();
    }

    public async Task<BrandDTO> GetByCode(string code)
    {
        var getByCode = await _dbSet.FirstOrDefaultAsync(x => x.Code == code);
        return getByCode;
    }

    public async Task<List<BrandDTO>> GetAllAndProduct()
    {
        var getAllAndProduct = await _context.Brand.Include(i => i.ListProduct).ToListAsync();
        return getAllAndProduct.Select(i => (BrandDTO)i).ToList();
    }

    public async Task<List<BrandDTO>> GetAndProduct(long id)
    {
        var getAndProduct = await _context.Brand.Include(i => i.ListProduct).Where(i => i.Id == id).ToListAsync();
        return getAndProduct.Select(i => (BrandDTO)i).ToList();
    }
}