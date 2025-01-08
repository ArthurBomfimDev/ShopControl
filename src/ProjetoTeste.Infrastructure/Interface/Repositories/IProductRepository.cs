using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> Exist(string code);
    Task<bool> ExistUpdate(string code, long id);
    Task<List<long>> BrandId(List<long> ids);
}
