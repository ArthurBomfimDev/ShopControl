using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository.Base;

namespace ShopControl.Domain.Interface.Repository;

public interface IBrandRepository : IBaseRepository<BrandDTO>
{
    Task<List<BrandDTO>> GetListByListCode(List<string> listCode);
    Task<BrandDTO> GetByCode(string code);
    Task<List<BrandDTO>> GetAllAndProduct();
    Task<List<BrandDTO>> GetAndProduct(long id);
}