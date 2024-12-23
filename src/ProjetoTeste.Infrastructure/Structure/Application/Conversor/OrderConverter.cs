using ProjetoEstagioAPI.Arguments.Order;
using ProjetoEstagioAPI.Models;

namespace ProjetoEstagioAPI.Mapping.Orders
{
    public static class OrderConverter
    {
        public static OutputOrder? ToOutputOrder(this Order? order)
        {
            if (order is null) return null;
            return new OutputOrder(order.Id, order.ClientId, order.ProductOrders, order.Total, order.OrderDate);
        }
        public static List<OutputOrder?> ToOutputOrderList(this List<Order?> order)
        {
            if (order is null) return null;
            return order.Select(o => new OutputOrder(o.Id, o.ClientId, o.ProductOrders, o.Total, o.OrderDate)).ToList();
        }
        public static Order? ToOrder(this InputCreateOrder order)
        {
            if (order is null) return null;
            return new Order(order.ClientId, order.OrderDate, order.ProductOrders);
        }
    }
}