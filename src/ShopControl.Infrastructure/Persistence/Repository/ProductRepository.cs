using Microsoft.EntityFrameworkCore;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository;
using ShopControl.Infrastructure.Persistence.Context;
using ShopControl.Infrastructure.Persistence.Entity;

namespace ShopControl.Infrastructure.Persistence.Repository
{
    public class ProductRepository : BaseRepository<Product, ProductDTO>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<ProductDTO>> GetListByCodeList(List<string> listCode)
        {
            var getListByCodeList = await _dbSet.Where(i => listCode.Contains(i.Code)).ToListAsync();
            return getListByCodeList.Select(i => (ProductDTO)i).ToList();
        }
        public async Task<List<ProductDTO>> GetListByBrandId(long id)
        {
            var getListByBrandId = await _dbSet.Where(i => i.BrandId == id).ToListAsync();
            return getListByBrandId.Select(i => (ProductDTO)i).ToList();
        }

        public Task<bool> ExistUpdate(string code, long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<long>> BrandId(List<long> ids)
        {
            return await _dbSet.Where(i => ids.Contains(i.BrandId)).Select(i => i.BrandId).ToListAsync();
        }

        public bool ProductExists(long id)
        {
            return _dbSet.Any(x => x.Id == id);
        }
    }
}