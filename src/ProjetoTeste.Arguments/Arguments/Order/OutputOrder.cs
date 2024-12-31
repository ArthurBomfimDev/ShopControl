using ProjetoTeste.Arguments.Arguments.ProductsOrder;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order;

public class OutputOrder
{
    public long Id { get; set; }
    public long ClientId { get; set; }
    public List<OutputProductOrder> ProductOrders { get; set; } = new List<OutputProductOrder>();
    public decimal Total { get; set; }
    public DateOnly OrderDate { get; set; }
    [JsonConstructor]
    public OutputOrder(long id, long clientId, List<OutputProductOrder> productOrders, decimal total, DateOnly orderDate)
    {
        Id = id;
        ClientId = clientId;
        ProductOrders = productOrders;
        Total = total;
        OrderDate = orderDate;
    }
}