using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Context;
using ProjetoTeste.Infrastructure.Default;
using ProjetoTeste.Models;

namespace ProjetoTeste.Infrastructure.Clients;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }
}
