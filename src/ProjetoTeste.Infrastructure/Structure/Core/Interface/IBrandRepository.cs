using ProjetoEstagioAPI.Infrastructure.Default;
using ProjetoEstagioAPI.Models;

namespace ProjetoEstagioAPI.Infrastructure.Brands
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<bool> Exist(string name);
    }
}
