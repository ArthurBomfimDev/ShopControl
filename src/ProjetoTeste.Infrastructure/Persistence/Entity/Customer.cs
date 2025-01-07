using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Customer : BaseEntity
{
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public List<Order>? ListOrder { get; set; }
    public Customer()
    { }

    public Customer(string name, string cPF, string email, string phone, List<Order>? listOrder)
    {
        Name = name;
        CPF = cPF;
        Email = email;
        Phone = phone;
        ListOrder = listOrder;
    }
}
