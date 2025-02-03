using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method: JsonConstructor]
public class InputIdentityViewProduct(long id) : BaseInputIdentityView<InputIdentityViewProduct>, IBaseIdentity
{
    public long Id { get; private set; } = id;
}