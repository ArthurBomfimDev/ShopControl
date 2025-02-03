using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Customer;

[method: JsonConstructor]
public class InputCreateCustomer(string name, string cPF, string email, string phone) : BaseInputCreate<InputCreateCustomer>
{
    public string Name { get; private set; } = name;
    public string CPF { get; private set; } = cPF;
    public string Email { get; private set; } = email;
    public string Phone { get; private set; } = phone;
}