using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method: JsonConstructor]
public class CustomerDTO(long id, string name, string cPF, string email, string phone)
{
    public long Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string CPF { get; set; } = cPF;
    public string Email { get; set; } = email;
    public string Phone { get; set; } = phone;

    public List<OrderDTO>? ListOrder { get; set; }
}