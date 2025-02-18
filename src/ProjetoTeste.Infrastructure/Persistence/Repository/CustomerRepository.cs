using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Domain.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class CustomerRepository : BaseRepository<Customer, CustomerDTO>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context)
    {
    }

    public bool CPFExists(string cpf)
    {
        return _dbSet.Any(x => x.CPF == cpf);

    }

    public bool EmailExists(string email)
    {
        return _dbSet.Any(x => x.Email == email);
    }

    public bool Exists(long id)
    {
        return _dbSet.Any(x => x.Id == id);
    }

    public bool PhoneExists(string phone)
    {
        return _dbSet.Any(x => x.Phone == phone);
    }

}