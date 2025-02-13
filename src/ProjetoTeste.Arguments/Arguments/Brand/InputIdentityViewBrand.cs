using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityViewBrand : BaseInputIdentityView<InputIdentityViewBrand>, IBaseIdentity
{
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