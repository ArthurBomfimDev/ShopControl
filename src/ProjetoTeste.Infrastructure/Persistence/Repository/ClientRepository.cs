using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> CPFExists(string cpf)
    {
        return await _dbSet.AnyAsync(x => x.CPF == cpf);

    }

    public async Task<bool> EmailExists(string email)
    {
        return await _dbSet.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> PhoneExists(string phone)
    {
        return await _dbSet.AnyAsync(x => x.Phone == phone);
    }

}