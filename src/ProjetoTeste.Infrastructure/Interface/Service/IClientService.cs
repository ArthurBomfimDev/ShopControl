using ProjetoTeste.Arguments.Arguments.Client;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Service;

public interface IClientService
{
    Task<Response<List<OutputClient>>> GetAll();
    Task<Response<OutputClient>> Get(long id);
    Task<Response<bool>> Delete(long id);
    Task<Response<OutputClient>> Create(InputCreateClient client);
    Task<Response<bool>> Update(long id, InputUpdateClient client);
}