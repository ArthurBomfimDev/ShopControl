using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Interface.Service.Base;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface ICustomerService : IBaseService<Customer, InputCreateCustomer, InputIdentityUpdateCustomer, InputIdentifyDeleteCustomer, InputIdentifyViewCustomer, OutputCustomer>
{ }