using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService
    {
        Task<List<OutputBrand>> GetAll();
        Task<OutputBrand> Get(InputIdentifyViewBrand inputIdentifyViewBrand);
        Task<List<OutputBrand>> GetListByListId(List<InputIdentifyViewBrand> listInputIdentifyViewBrand);
        Task<BaseResponse<OutputBrand>> Create(InputCreateBrand inputCreateBrand);
        Task<BaseResponse<List<OutputBrand>>> CreateMultiple(List<InputCreateBrand> listInputCreateBrand);
        Task<BaseResponse<bool>> Update(InputIdentityUpdateBrand inputIdentityUpdateBrand);
        Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand);
        Task<BaseResponse<bool>> Delete(InputIdentifyDeleteBrand inputIdentifyDeleteBrand);
        Task<BaseResponse<bool>> DeleteMultiple(List<InputIdentifyDeleteBrand> listInputIdentifyDeleteBrand);
    }
}