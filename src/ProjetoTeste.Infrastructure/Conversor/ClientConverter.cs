using ProjetoTeste.Arguments.Arguments.Client;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor;

public static class ClientConverter
{
    public static OutputClient? ToOutputClient(this Client client)
    {
        return client == null ? null : new OutputClient(client.Id, client.Name, client.Phone, client.CPF, client.Email);
    }

    public static Client? ToClient(this InputCreateClient client)
    {
        return client == null ? null : new Client(client.Name, client.CPF, client.Email, client.Phone);
    }
}