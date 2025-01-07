using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Customer;

[method: JsonConstructor]
public class InputCreateCustomer(string name, string cPF, string email, string phone)
{
    public string Name { get; private set; } = name;
    [Required]
    [MaxLength(11, ErrorMessage = "O CPF deve ter no maximo 11 digitos")]
    public string CPF { get; private set; } = cPF;
    [Required]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
    public string Email { get; private set; } = email;
    [MaxLength(15, ErrorMessage = "O telefone deve ter no maximo 11 digitos")]
    public string Phone { get; private set; } = phone;
}