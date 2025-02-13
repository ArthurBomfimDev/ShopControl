using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityDeleteProduct : BaseInputIdentityDelete<InputIdentityDeleteProduct>
{
    public long Id { get; set; }
    public InputIdentityDeleteProduct()
    {

    }

    [JsonConstructor]
    public InputIdentityDeleteProduct(long id)
    {
        Id = id;
    }
}
