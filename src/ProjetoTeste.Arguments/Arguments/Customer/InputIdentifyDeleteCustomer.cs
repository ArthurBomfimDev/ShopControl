using ProjetoTeste.Arguments.Arguments.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentifyDeleteCustomer : BaseInputIdentityDelete<InputIdentifyDeleteCustomer>
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long Id { get; private set; }

    public InputIdentifyDeleteCustomer()
    {

    }

    [JsonConstructor]
    public InputIdentifyDeleteCustomer(long id)
    {
        Id = id;
    }
}