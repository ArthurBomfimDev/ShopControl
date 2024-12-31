using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.ProductsOrder;

[method: JsonConstructor]
public class OutputProductOrder(long orderId, long productId, int quantity)
{
    public long OrderId { get; private set; } = orderId;
    public long ProductId { get; private set; } = productId;
    public int Quantity { get; private set; } = quantity;
}