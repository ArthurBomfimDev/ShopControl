using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

[method: JsonConstructor]
public class HighestAverageSalesValue(long orderId, decimal avaragePrice, decimal totalSaleValue, int totalSaleQuantity)
{
    public long OrderId { get; private set; } = orderId;
    public decimal AvaragePrice { get; private set; } = avaragePrice;
    public decimal TotalSaleValue { get; private set; } = totalSaleValue;
    public int TotalSaleQuantity { get; private set; } = totalSaleQuantity;
}
