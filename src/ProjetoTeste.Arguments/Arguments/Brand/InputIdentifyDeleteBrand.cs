using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentifyDeleteBrand : BaseInputIdentityDelete<InputIdentifyDeleteBrand>
{
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
