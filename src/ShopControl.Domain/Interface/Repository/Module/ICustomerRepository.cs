using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository.Base;

namespace ShopControl.Domain.Interface.Repository;

public interface ICustomerRepository : IBaseRepository<CustomerDTO>
{
    bool EmailExists(string email);
    bool CPFExists(string cpf);
    bool PhoneExists(string phone);
    bool Exists(long id);
    Task<List<CustomerDTO>> GetListCustumerByListCPF(List<string> listCPF);
}