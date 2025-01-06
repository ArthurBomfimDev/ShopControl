using ProjetoTeste.Arguments.Arguments.Client;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor;

public static class CustomerConverter
{
    public static OutputCustomer? ToOutputCustomer(this Customer customer)
    {
        return customer == null ? null : new OutputCustomer(customer.Id, customer.Name, customer.CPF, customer.Email, customer.Phone);
    }

    public static Customer? ToCustomer(this InputCreateCustomer customer)
    {
        return customer == null ? null : new Customer(customer.Name, customer.CPF, customer.Email, customer.Phone);
    }
}