namespace ProjetoEstagioAPI.Models;

public class Client : BaseEntity
{
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }

    public List<Order?> Order { get; set; }
    public Client()
    { }
}
