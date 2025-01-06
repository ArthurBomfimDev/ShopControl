using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order;

[method: JsonConstructor]
public class OutputClientOrder(long clientId, string name, IEnumerable<long> totalOrders, decimal quantityPurchased, decimal totalPrice)
{
    public long ClientId { get; private set; } = clientId;
    public string Name { get; private set; } = name;
    public long TotalOrders { get; private set; } = totalOrders.Count();
    public decimal QuantityPurchased { get; private set; } = quantityPurchased;
    public decimal TotalPrice { get; private set; } = totalPrice;
}
