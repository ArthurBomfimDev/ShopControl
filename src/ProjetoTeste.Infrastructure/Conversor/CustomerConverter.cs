using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor;

public static class CustomerConverter
{
    public static OutputCustomer? ToOutputCustomer(this Customer customer)
    {
        return customer == null ? null : new OutputCustomer(customer.Id, customer.Name, customer.CPF, customer.Email, customer.Phone);
    }

    public static CustomerDTO? ToCustomerDTO(this Customer customer)
    {
        return customer == null ? null : new CustomerDTO(customer.Id, customer.Name, customer.CPF, customer.Email, customer.Phone);
    }
}