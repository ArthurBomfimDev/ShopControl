using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentifyDeleteCustomer : BaseInputIdentityDelete<InputIdentifyDeleteCustomer>
{
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