using Microsoft.EntityFrameworkCore;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository;
using ShopControl.Infrastructure.Persistence.Context;
using ShopControl.Infrastructure.Persistence.Entity;

namespace ShopControl.Infrastructure.Persistence.Repository;

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

    public async Task<List<CustomerDTO>> GetListCustumerByListCPF(List<string> listCPF)
    {
        var listCustomer = await _dbSet.Where(i => listCPF.Contains(i.CPF)).AsNoTracking().ToListAsync();
        return listCustomer.Select(i => (CustomerDTO)i).ToList();
    }

    public bool PhoneExists(string phone)
    {
        return _dbSet.Any(x => x.Phone == phone);
    }

}