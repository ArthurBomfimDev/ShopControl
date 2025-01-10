using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Repositories;

public interface IProductRepository : IRepository<Product>
{
    bool CodeExists(string code);
    bool ProductExists(long id);
    Task<bool> ExistUpdate(string code, long id);
    Task<List<long>> BrandId(List<long> ids);
}
