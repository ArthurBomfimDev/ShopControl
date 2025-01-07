using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface ICustomerService
{
    Task<BaseResponse<List<OutputCustomer>>> GetAll();
    Task<BaseResponse<OutputCustomer>> Get(long id);
    Task<BaseResponse<bool>> Delete(long id);
    Task<BaseResponse<OutputCustomer>> Create(InputCreateCustomer client);
    Task<BaseResponse<bool>> Update(long id, InputUpdateCustomer client);
}