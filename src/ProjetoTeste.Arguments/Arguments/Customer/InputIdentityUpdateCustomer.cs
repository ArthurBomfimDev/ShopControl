using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityUpdateCustomer : BaseInputIdentityUpdate<InputIdentityUpdateCustomer>
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long Id { get; private set; }
    public InputUpdateCustomer InputUpdateCustomer { get; private set; }

    public InputIdentityUpdateCustomer()
    {

    }

    [JsonConstructor]
    public InputIdentityUpdateCustomer(long id, InputUpdateCustomer inputUpdateCustomer)
    {
        Id = id;
        InputUpdateCustomer = inputUpdateCustomer;
    }
}
