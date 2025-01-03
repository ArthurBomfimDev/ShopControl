using ProjetoTeste.Arguments.Arguments.Brands;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService
    {
        Task<Response<List<OutputBrand>>> GetAll();
        Task<Response<OutputBrand>> Get(long id);
        Task<Response<OutputBrand>> Create(InputCreateBrand input);
        Task<Response<bool>> Update(long id, InputUpdateBrand brand);
        Task<Response<bool>> Delete(long id);
    }
}
