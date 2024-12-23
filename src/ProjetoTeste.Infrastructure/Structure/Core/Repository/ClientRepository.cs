using Microsoft.EntityFrameworkCore;
using ProjetoEstagioAPI.Context;
using ProjetoEstagioAPI.Infrastructure.Default;
using ProjetoEstagioAPI.Models;

namespace ProjetoEstagioAPI.Infrastructure.Clients;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }
}
