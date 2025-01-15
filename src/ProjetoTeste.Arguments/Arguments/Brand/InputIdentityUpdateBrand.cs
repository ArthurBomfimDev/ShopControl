using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Product;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method: JsonConstructor]
public class InputIdentityUpdateBrand(long id, InputUpdateProduct inputUpdateProduct)
{
    public long Id { get; private set; } = id;
    public InputUpdateProduct InputUpdateProduct { get; private set; } = inputUpdateProduct;
}