using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class OrderConverter
    {
        public static OutputOrder? ToOutputOrder(this Order? order)
        {
            if (order is null) return null;
            return order == null ? null : new OutputOrder(order.Id, order.ClientId, (from i in order.ProductOrders select i.ToOuputProductOrder()).ToList(), order.Total, order.OrderDate);
        }

        public static Order? ToOrder(this InputCreateOrder order)
        {
            return order == null ? null : new Order(order.ClientId, DateOnly.FromDateTime(DateTime.Now));
        }
    }
}