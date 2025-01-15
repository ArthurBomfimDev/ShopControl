using ProjetoTeste.Arguments.Arguments.Product;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method: JsonConstructor]
public class InputIdentityUpdateProduct(long id, InputUpdateProduct inputUpdateProduct)
{
    public long Id { get; private set; } = id;
    public InputUpdateProduct InputUpdateProduct { get; private set; } = inputUpdateProduct;
}