using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Repositories;

public interface IBrandRepository : IRepository<Brand>
{
    Task<bool> Exist(List<string> code);
    Task<List<Brand>> GetAllAndProduct();
    Task<List<Brand>> GetAndProduct(long id);
}