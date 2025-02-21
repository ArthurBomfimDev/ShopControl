using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Infrastructure.Interface.Service.Base;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface ICustomerService : IBaseService<CustomerDTO, InputCreateCustomer, InputIdentityUpdateCustomer, InputIdentifyDeleteCustomer, InputIdentifyViewCustomer, OutputCustomer>
{ }