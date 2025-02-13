using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityViewProduct : BaseInputIdentityView<InputIdentityViewProduct>, IBaseIdentity
{
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