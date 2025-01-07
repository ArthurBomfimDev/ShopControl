using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brands;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService
    {
        Task<BaseResponse<List<OutputBrand>>> GetAll();
        Task<BaseResponse<OutputBrand>> Get(long id);
        Task<BaseResponse<OutputBrand>> Create(InputCreateBrand input);
        Task<BaseResponse<bool>> Update(long id, InputUpdateBrand brand);
        Task<BaseResponse<bool>> Delete(long id);
    }
}
