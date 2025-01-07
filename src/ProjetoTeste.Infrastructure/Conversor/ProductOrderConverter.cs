using ProjetoTeste.Arguments.Arguments.ProductsOrder;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class ProductOrderConverter
    {
        public static OutputProductOrder? ToOuputProductOrder(this ProductOrder? productOrders)
        {
            return productOrders == null ? default : new OutputProductOrder(productOrders.OrderId, productOrders.ProductId, productOrders.UnitPrice, productOrders.Quantity, productOrders.SubTotal);
        }
    }
}