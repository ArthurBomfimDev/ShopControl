using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.ProductOrder;
using System.Text.Json.Serialization;

namespace ShopControl.Arguments.Arguments.Order;

public class OutputOrder : BaseOutput<OutputOrder>
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OutputProductOrder> ListProductOrders { get; set; }

    public OutputOrder() { }

    [JsonConstructor]
    public OutputOrder(long id, long customerId, List<OutputProductOrder> productOrders, decimal total, DateTime orderDate)
    {
        Id = id;
        CustomerId = customerId;
        ListProductOrders = productOrders;
        Total = total;
        OrderDate = orderDate;

    }
}