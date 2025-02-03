using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.ProductOrder;

[method: JsonConstructor]
public class OutputProductOrder(long orderId, long productId, decimal unitPrice, int quantity, decimal subTotal) : BaseOutput<OutputProductOrder>
{
    public long OrderId { get; private set; } = orderId;
    public long ProductId { get; private set; } = productId;
    public decimal UnitPrice { get; private set; } = unitPrice;
    public int Quantity { get; private set; } = quantity;
    public decimal SubTotal { get; private set; } = subTotal;
}