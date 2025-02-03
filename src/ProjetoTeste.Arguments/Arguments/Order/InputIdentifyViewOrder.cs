using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method: JsonConstructor]
public class InputIdentifyViewOrder(long id) : BaseInputIdentityView<InputIdentifyViewOrder>, IBaseIdentity
{
    public long Id { get; private set; } = id;
}