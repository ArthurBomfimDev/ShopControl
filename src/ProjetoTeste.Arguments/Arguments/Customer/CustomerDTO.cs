using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method: JsonConstructor]
public class CustomerDTO(string name, string cPF, string email, string phone)
{
    public string Name { get; private set; } = name;
    public string CPF { get; private set; } = cPF;
    public string Email { get; private set; } = email;
    public string Phone { get; private set; } = phone;

    public List<OrderDTO>? ListOrder { get; private set; }
}