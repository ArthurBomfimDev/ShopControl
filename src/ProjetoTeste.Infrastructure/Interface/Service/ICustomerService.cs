using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface ICustomerService
{
    Task<BaseResponse<List<OutputCustomer>>> GetAll();
    Task<BaseResponse<List<OutputCustomer>>> Get(List<long> idList);
    Task<BaseResponse<bool>> Delete(List<long> id);
    Task<BaseResponse<List<OutputCustomer>>> Create(List<InputCreateCustomer> client);
    Task<BaseResponse<bool>> Update(List<long> idList, List<InputUpdateCustomer> client);
}