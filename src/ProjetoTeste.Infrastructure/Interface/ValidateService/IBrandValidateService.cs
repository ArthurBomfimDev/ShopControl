using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface IBrandValidateService
{
    Task<BaseResponse<List<BrandValidateDTO>>> ValidateCreate(List<BrandValidateDTO> listBrandValidate);
    Task<BaseResponse<List<BrandValidateDTO?>>> ValidateUpdate(List<BrandValidateDTO> listBrandValidate);
    Task<BaseResponse<List<BrandValidateDTO>>?> ValidateDelete(List<BrandValidateDTO> listBrandValidate);
}