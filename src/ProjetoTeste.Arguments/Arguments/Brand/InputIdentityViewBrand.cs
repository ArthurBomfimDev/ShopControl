using ProjetoTeste.Arguments.Arguments.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityViewBrand : BaseInputIdentityView<InputIdentityViewBrand>, IBaseIdentity
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long Id { get; private set; }

    public InputIdentityViewBrand()
    {

    }

    [JsonConstructor]
    public InputIdentityViewBrand(long id)
    {
        Id = id;
    }
}