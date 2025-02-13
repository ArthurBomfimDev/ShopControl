using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Customer : BaseEntity
{
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public virtual List<Order>? ListOrder { get; set; }

    public Customer()
    { }

    public Customer(string name, string cPF, string email, string phone)
    {
        Name = name;
        CPF = cPF;
        Email = email;
        Phone = phone;
    }

    #region Implicit Conversor
    public static implicit operator Customer(CustomerDTO customerDTO)
    {
        return customerDTO != null ? new Customer
        {
            Id = customerDTO.Id,
            Name = customerDTO.Name,
            CPF = customerDTO.CPF,
            Email = customerDTO.Email,
            Phone = customerDTO.Phone,
            ListOrder = customerDTO.ListOrder.Select(i => (Order)i).ToList()
        } : null;
    }
    public static implicit operator CustomerDTO(Customer customer)
    {
        return customer != null ? new CustomerDTO
        (
            customer.Id,
            customer.Name,
            customer.CPF,
            customer.Email,
            customer.Phone,
            customer.ListOrder.Select(i => (OrderDTO)i).ToList()
        ) : null;
    }
    #endregion
}