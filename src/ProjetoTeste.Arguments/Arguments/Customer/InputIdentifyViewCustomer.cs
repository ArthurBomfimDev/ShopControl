using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentifyViewCustomer : BaseInputIdentityView<InputIdentifyViewCustomer>, IBaseIdentity
{
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