using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface ICustomerValidateService
{
    Task<BaseResponse<List<CustomerValidateDTO>>> ValidateCreate(List<CustomerValidateDTO> listCustomerValidate);
    Task<BaseResponse<List<CustomerValidateDTO>>> ValidateUpdate(List<CustomerValidateDTO> listCustomerValidate);
    Task<BaseResponse<List<CustomerValidateDTO>>> ValidateDelete(List<CustomerValidateDTO> listCustomerValidate);
}