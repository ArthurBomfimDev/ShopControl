using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface IBrandValidateService
{
    Task<BaseResponse<List<BrandValidate>>> ValidateCreate(List<BrandValidate> listBrandValidate);
    Task<BaseResponse<List<BrandValidate?>>> ValidateUpdate(List<BrandValidate> listBrandValidate);
    Task<BaseResponse<List<long>>?> ValidateDelete(List<BrandValidate> listBrandValidate);
}