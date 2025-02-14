using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface IProductValidateService
{
    Task<BaseResponse<List<ProductValidateDTO>>> ValidateCreate(List<ProductValidateDTO> listProductValidate);
    Task<BaseResponse<List<ProductValidateDTO>>> ValidateUpdate(List<ProductValidateDTO> listProductValidate);
    Task<BaseResponse<List<ProductValidateDTO>>> ValidateDelete(List<ProductValidateDTO> listProductValidate);
}