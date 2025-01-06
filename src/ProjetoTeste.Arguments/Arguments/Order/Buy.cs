using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order;

[method: JsonConstructor]
public class Buy(long clintId, IEnumerable<long> orders, int totalBuyer, decimal totalPrice)
{
    public long ClientId { get; private set; } = clintId;
    public IEnumerable<long> Orders { get; private set; } = orders;
    public int TotalBuyer { get; private set; } = totalBuyer;
    public decimal TotalPrice { get; private set; } = totalPrice;
}