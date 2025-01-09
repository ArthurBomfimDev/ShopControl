using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Interface.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        bool EmailExists(string email);
        bool CPFExists(string cpf);
        bool PhoneExists(string phone);
        bool Exists(long id);
    }
}
