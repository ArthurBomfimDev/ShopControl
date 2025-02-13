using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class CustomerDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public CustomerDTO()
    {
        
    }

    public List<OrderDTO>? ListOrder { get; set; }

    [JsonConstructor]
    public CustomerDTO(long id, string name, string cPF, string email, string phone)
    {
        Id = id;
        Name = name;
        CPF = cPF;
        Email = email;
        Phone = phone;
    }
}