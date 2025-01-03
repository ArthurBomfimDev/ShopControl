using ProjetoTeste.Arguments.Arguments.Client;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor;

public static class ClientConverter
{
    public static OutputClient? ToOutputClient(this Client client)
    {
        if (client == null) return null;
        return new OutputClient(client.Id, client.Name, client.Phone, client.CPF, client.Email);
    }
    public static List<OutputClient?> ToOutputClientList(this List<Client> client)
    {
        if (client == null) return null;
        return client.Select(c => new OutputClient(c.Id, c.Name, c.Phone, c.CPF, c.Email)).ToList();
    }
    public static Client? ToClient(this InputCreateClient client)
    {
        if (client == null) return null;
        return new Client(client.Name, client.CPF, client.Email, client.Phone);
    }
}