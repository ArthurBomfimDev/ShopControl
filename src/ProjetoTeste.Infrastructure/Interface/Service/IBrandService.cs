using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService
    {
        Task<List<OutputBrand>> GetAll();
        Task<OutputBrand> Get(long id);
        Task<List<OutputBrand>> GetListByListId(List<long> ids);
        //Task<List<OutputBrand>> GetAllAndProduct();
        //Task<List<OutputBrand>> GetAndProduct(long id);
        Task<BaseResponse<List<OutputBrand>>> Create(List<InputCreateBrand> input);
        Task<BaseResponse<bool>> Update(List<long> ids, List<InputUpdateBrand> brand);
        Task<BaseResponse<bool>> Delete(List<long> ids);
    }
}