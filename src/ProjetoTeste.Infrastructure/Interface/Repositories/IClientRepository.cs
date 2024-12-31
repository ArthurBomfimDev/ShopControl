using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<bool> EmailExists(string email);
        Task<bool> CPFExists(string cpf);
        Task<bool> PhoneExists(string phone);
    }
}
