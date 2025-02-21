using ProjetoTeste.Arguments.Arguments.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentifyDeleteBrand : BaseInputIdentityDelete<InputIdentifyDeleteBrand>
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long Id { get; private set; }

    public InputIdentifyDeleteBrand()
    {

    }

    [JsonConstructor]
    public InputIdentifyDeleteBrand(long id)
    {
        Id = id;
    }
}
