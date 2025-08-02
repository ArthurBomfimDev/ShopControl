using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository.Base;

namespace ShopControl.Domain.Interface.Repository;
public interface IProductRepository : IBaseRepository<ProductDTO>
{
    Task<List<ProductDTO>> GetListByBrandId(long id);
    Task<List<ProductDTO>> GetListByCodeList(List<string> listCode);
    bool ProductExists(long id);
    Task<bool> ExistUpdate(string code, long id);
    Task<List<long>> BrandId(List<long> ids);
}