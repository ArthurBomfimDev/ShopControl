using ProjetoTeste.Arguments.Arguments.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentifyViewOrder : BaseInputIdentityView<InputIdentifyViewOrder>, IBaseIdentity
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long Id { get; private set; }

    public InputIdentifyViewOrder()
    {

    }

    [JsonConstructor]
    public InputIdentifyViewOrder(long id)
    {
        Id = id;
    }
}