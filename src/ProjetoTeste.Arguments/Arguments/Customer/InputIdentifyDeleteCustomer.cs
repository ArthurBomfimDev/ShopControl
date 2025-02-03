using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method: JsonConstructor]
public class InputIdentifyDeleteCustomer(long id) : BaseInputIdentityDelete<InputIdentifyDeleteCustomer>
{
    public long Id { get; private set; } = id;
}