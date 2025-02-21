using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Customer;

public class InputUpdateCustomer
{
    public string Name { get; private set; }
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public string CPF { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }

    public InputUpdateCustomer()
    {

    }

    [JsonConstructor]
    public InputUpdateCustomer(string name, string cPF, string email, string phone)
    {
        Name = name;
        CPF = cPF;
        Email = email;
        Phone = phone;
    }
}