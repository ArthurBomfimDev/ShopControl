using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order;

[method: JsonConstructor]
public class ProductSell(long productId, int totalSeller, decimal totalPrice)
{
    public long productId { get; private set; } = productId;
    public int totalSeller { get; private set; } = totalSeller;
    public decimal totalPrice { get; private set; } = totalPrice;
}
