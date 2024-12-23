using ProjetoTeste.Infrastructure.Default;
using ProjetoTeste.Models;

namespace ProjetoTeste.Infrastructure.Brands
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<bool> Exist(string name);
    }
}
