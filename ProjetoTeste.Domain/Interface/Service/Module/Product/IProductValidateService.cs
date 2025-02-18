using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Domain.Interface.Service.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface IProductValidateService : IBaseValidateService
{
    Task<BaseResponse<List<ProductValidateDTO>>> ValidateCreate(List<ProductValidateDTO> listProductValidate);
    Task<BaseResponse<List<ProductValidateDTO>>> ValidateUpdate(List<ProductValidateDTO> listProductValidate);
    Task<BaseResponse<List<ProductValidateDTO>>> ValidateDelete(List<ProductValidateDTO> listProductValidate);
}