using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method: JsonConstructor]
public class InputIdentityViewBrand(long id) : BaseInputIdentityView<InputIdentityViewBrand>, IBaseIdentity
{
    public long Id { get; private set; } = id;
}