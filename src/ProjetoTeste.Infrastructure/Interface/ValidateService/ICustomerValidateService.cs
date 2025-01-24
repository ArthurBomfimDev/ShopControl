using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Infrastructure.Interface.ValidateService;

public interface ICustomerValidateService
{
    Task<BaseResponse<List<CustomerValidate>>> ValidateCreate(List<CustomerValidate> listCustomerValidate);
    Task<BaseResponse<List<CustomerValidate>>> ValidateUpdate(List<CustomerValidate> listCustomerValidate);
    Task<BaseResponse<List<CustomerValidate>>> ValidateDelete(List<CustomerValidate> listCustomerValidate);
}