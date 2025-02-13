using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityUpdateBrand : BaseInputIdentityUpdate<InputIdentityUpdateBrand>
{
    public long Id { get; private set; }
    public InputUpdateBrand InputUpdateBrand { get; private set; }

    public InputIdentityUpdateBrand()
    {

    }

    [JsonConstructor]
    public InputIdentityUpdateBrand(long id, InputUpdateBrand inputUpdateBrand)
    {
        Id = id;
        InputUpdateBrand = inputUpdateBrand;
    }
}