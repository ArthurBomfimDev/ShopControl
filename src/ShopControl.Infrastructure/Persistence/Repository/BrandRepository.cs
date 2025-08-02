using Microsoft.EntityFrameworkCore;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository;
using ShopControl.Infrastructure.Persistence.Context;
using ShopControl.Infrastructure.Persistence.Entity;

namespace ShopControl.Infrastructure.Persistence.Repository;

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