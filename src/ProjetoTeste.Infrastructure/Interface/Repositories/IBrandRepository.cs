using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<bool> Exist(string name);
    }
}
