using ProjetoTeste.Arguments.Arguments.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityViewProduct : BaseInputIdentityView<InputIdentityViewProduct>, IBaseIdentity
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long Id { get; private set; }

    public InputIdentityViewProduct()
    {

    }

    [JsonConstructor]
    public InputIdentityViewProduct(long id)
    {
        Id = id;
    }
}