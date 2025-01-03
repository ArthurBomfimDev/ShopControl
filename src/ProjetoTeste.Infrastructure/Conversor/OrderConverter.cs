using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class OrderConverter
    {
        public static OutputOrder? ToOutputOrder(this Order? order)
        {
            if (order is null) return null;
            return new OutputOrder(order.Id, order.ClientId, default/*order.ProductOrders.ToOuputProductOrder()*/, order.Total, order.OrderDate);
        }

        public static Order? ToOrder(this InputCreateOrder order)
        {
            if (order is null) return null;
            return new Order(order.ClientId, DateOnly.FromDateTime(DateTime.Now));
        }
    }
}