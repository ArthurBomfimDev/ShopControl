using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.ProductOrder;

[method: JsonConstructor]
public class OutputProductOrder(long orderId, long productId, int quantity, decimal unitPrice, decimal subTotal) : BaseOutput<OutputProductOrder>
{
    public long OrderId { get; private set; } = orderId;
    public long ProductId { get; private set; } = productId;
    public int Quantity { get; private set; } = quantity;
    public decimal UnitPrice { get; private set; } = unitPrice;
    public decimal SubTotal { get; private set; } = subTotal;
}