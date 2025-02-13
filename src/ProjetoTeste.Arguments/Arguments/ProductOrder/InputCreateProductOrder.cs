using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.ProductOrder;

public class InputCreateProductOrder : BaseInputCreate<InputCreateProductOrder>
{
    public long OrderId { get; private set; }
    public long ProductId { get; private set; }
    public int Quantity { get; private set; }

    public InputCreateProductOrder()
    {

    }

    [JsonConstructor]
    public InputCreateProductOrder(long orderId, long productId, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }
}