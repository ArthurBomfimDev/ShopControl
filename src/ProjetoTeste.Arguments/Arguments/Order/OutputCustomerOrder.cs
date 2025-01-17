using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order;

public class OutputCustomerOrder
{
    public long CustomerId { get; private set; }
    public string Name { get; private set; }
    public long TotalOrders { get; private set; }
    public decimal QuantityPurchased { get; private set; }
    public decimal TotalPrice { get; private set; }

    public OutputCustomerOrder(long customerId, long totalOrders, decimal quantityPurchased, decimal totalPrice)
    {
        CustomerId = customerId;
        TotalOrders = totalOrders;
        QuantityPurchased = quantityPurchased;
        TotalPrice = totalPrice;
    }

    [JsonConstructor]
    public OutputCustomerOrder(long customerId, string name, long totalOrders, decimal quantityPurchased, decimal totalPrice)
    {
        CustomerId = customerId;
        Name = name;
        TotalOrders = totalOrders;
        QuantityPurchased = quantityPurchased;
        TotalPrice = totalPrice;
    }
}
