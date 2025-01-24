using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface IProductValidateService
{
    Task<BaseResponse<List<ProductValidate>>> ValidateCreate(List<ProductValidate> listProductValidate);
    Task<BaseResponse<List<ProductValidate>>> ValidateUpdate(List<ProductValidate> listProductValidate);
    Task<BaseResponse<List<ProductValidate>>> ValidateDelete(List<ProductValidate> listProductValidate);
}