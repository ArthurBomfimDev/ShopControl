using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductsOrder;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class OrderConverter
    {
        public static OutputOrder? ToOutputOrder(this Order? order)
        {
            if (order is null) return null;
            return new OutputOrder(order.Id, order.ClientId, order.ProductOrders.ToOuputProductOrder(), order.Total, order.OrderDate);
        }
        public static List<OutputOrder?> ToOutputOrderList(this List<Order?> order)
        {
            if (order is null) return null;
            return order.Select(o => new OutputOrder(o.Id, o.ClientId, o.ProductOrders.ToOuputProductOrder(), o.Total, o.OrderDate)).ToList();
        }
        public static Order? ToOrder(this InputCreateOrder order)
        {
            if (order is null) return null;
            return new Order(order.ClientId, DateOnly.FromDateTime(DateTime.Now));
        }
    }
}