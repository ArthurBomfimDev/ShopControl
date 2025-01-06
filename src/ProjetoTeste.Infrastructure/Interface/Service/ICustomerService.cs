using ProjetoTeste.Arguments.Arguments.Client;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface ICustomerService
{
    Task<Response<List<OutputCustomer>>> GetAll();
    Task<Response<OutputCustomer>> Get(long id);
    Task<Response<bool>> Delete(long id);
    Task<Response<OutputCustomer>> Create(InputCreateCustomer client);
    Task<Response<bool>> Update(long id, InputUpdateCustomer client);
}