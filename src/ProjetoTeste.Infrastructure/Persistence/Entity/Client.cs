namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Client : BaseEntity
{
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public List<Order?> Order { get; set; }
    public Client()
    { }

    public Client(string name, string cPF, string email, string phone)
    {
        Name = name;
        CPF = cPF;
        Email = email;
        Phone = phone;
    }
}
