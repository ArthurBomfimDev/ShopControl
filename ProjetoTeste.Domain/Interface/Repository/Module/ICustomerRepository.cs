using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository.Base;

namespace ProjetoTeste.Domain.Interface.Repository;

public interface ICustomerRepository : IBaseRepository<CustomerDTO>
{
    bool EmailExists(string email);
    bool CPFExists(string cpf);
    bool PhoneExists(string phone);
    bool Exists(long id);
}