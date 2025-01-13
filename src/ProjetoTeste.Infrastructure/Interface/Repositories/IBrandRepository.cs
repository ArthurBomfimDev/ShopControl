using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Repositories;

public interface IBrandRepository : IRepository<Brand>
{
    Task<List<Brand>> GetListByListICode(List<string> listCode);
    Task<Brand> GetByCode(string code);
    Task<List<Brand>> GetAllAndProduct();
    Task<List<Brand>> GetAndProduct(long id);
    //bool BrandExists(long id);
}