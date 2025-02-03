using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method: JsonConstructor]
public class InputIdentifyDeleteBrand(long id) : BaseInputIdentityDelete<InputIdentifyDeleteBrand>
{
    public long Id { get; private set; } = id;
}
