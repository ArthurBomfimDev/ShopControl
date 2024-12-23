using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Context;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }
}
