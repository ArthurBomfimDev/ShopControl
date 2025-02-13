using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.ProductOrder;

public class OutputProductOrder : BaseOutput<OutputProductOrder>
{
    public long OrderId { get; private set; }
    public long ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal SubTotal { get; private set; }

    public OutputProductOrder()
    {
        
    }

    [JsonConstructor]
    public OutputProductOrder(long orderId, long productId, int quantity, decimal unitPrice, decimal subTotal)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        SubTotal = subTotal;
    }
}