using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method : JsonConstructor]
public class InputIdentifyViewCustomer(long id) : BaseInputIdentityView<InputIdentifyViewCustomer>, IBaseIdentity
{
    public long Id { get; private set; } = id;
}