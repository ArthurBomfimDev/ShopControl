using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface ICustomerService
{
    Task<List<OutputCustomer>> GetAll();
    Task<OutputCustomer> Get(long id);
    Task<List<OutputCustomer>> GetListByListId(List<long> listId);
    Task<BaseResponse<OutputCustomer>> Create(InputCreateCustomer inputCreateCustomer);
    Task<BaseResponse<List<OutputCustomer>>> CreateMultiple(List<InputCreateCustomer> listInputCreateCustomer);
    Task<BaseResponse<bool>> Update(InputIdentityUpdateCustomer inputIdentityUpdateCustomer);
    Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer);
    Task<BaseResponse<bool>> Delete(long id);
    Task<BaseResponse<bool>> DeleteMultiple(List<long> listId);
}