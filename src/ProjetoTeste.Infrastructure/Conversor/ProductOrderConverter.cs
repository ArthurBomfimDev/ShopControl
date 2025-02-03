using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class ProductOrderConverter
    {
        public static OutputProductOrder? ToOuputProductOrder(this ProductOrder? productOrders)
        {
            return productOrders == null ? default : new OutputProductOrder(productOrders.OrderId, productOrders.ProductId, productOrders.Quantity, productOrders.UnitPrice, productOrders.SubTotal);
        }
    }
}