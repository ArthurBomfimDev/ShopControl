using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository.Base;

namespace ProjetoTeste.Domain.Interface.Repository;

public interface IBrandRepository : IBaseRepository<BrandDTO>
{
    Task<List<BrandDTO>> GetListByListCode(List<string> listCode);
    Task<BrandDTO> GetByCode(string code);
    Task<List<BrandDTO>> GetAllAndProduct();
    Task<List<BrandDTO>> GetAndProduct(long id);
}