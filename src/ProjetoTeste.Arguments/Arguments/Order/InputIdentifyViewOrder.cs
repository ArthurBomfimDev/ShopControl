using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentifyViewOrder : BaseInputIdentityView<InputIdentifyViewOrder>, IBaseIdentity
{
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