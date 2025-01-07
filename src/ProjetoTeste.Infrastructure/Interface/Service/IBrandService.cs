using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brands;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService
    {
        Task<List<OutputBrand>> GetAll();
        Task<OutputBrand> Get(long id);
        Task<List<OutputBrand>> GetAllAndProduct();
        Task<List<OutputBrand>> GetAndProduct(long id);
        Task<BaseResponse<OutputBrand>> Create(InputCreateBrand input);
        Task<BaseResponse<bool>> Update(long id, InputUpdateBrand brand);
        Task<BaseResponse<bool>> Delete(long id);
    }
}