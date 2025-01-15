using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetListByBrandId(long id);
    Task<List<Product>> GetListByCodeList(List<string> listCode);
    bool ProductExists(long id);
    Task<bool> ExistUpdate(string code, long id);
    Task<List<long>> BrandId(List<long> ids);
}