using ShopControl.Arguments.Arguments.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShopControl.Arguments.Arguments;

public class InputIdentifyViewCustomer : BaseInputIdentityView<InputIdentifyViewCustomer>, IBaseIdentity
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long Id { get; private set; }

    public InputIdentifyViewCustomer()
    {

    }

    [JsonConstructor]
    public InputIdentifyViewCustomer(long id)
    {
        Id = id;
    }
}