using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.ProductOrder;

[method: JsonConstructor]
public class InputCreateProductOrder(long orderId, long productId, int quantity) : BaseInputCreate<InputCreateProductOrder>
{
    public long OrderId { get; private set; } = orderId;
    public long ProductId { get; private set; } = productId;
    public int Quantity { get; private set; } = quantity;
}