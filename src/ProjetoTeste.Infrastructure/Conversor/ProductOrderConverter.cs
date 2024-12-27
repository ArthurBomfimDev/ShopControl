using ProjetoTeste.Arguments.Arguments.Products;
using ProjetoTeste.Arguments.Arguments.ProductsOrder;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTeste.Infrastructure.Conversor;

public static class ProductOrderConverter
{
    public static List<ProductOrder?> ToProductOrder(this List<InputCreateProductOrder?> input)
    {
        if (input is null) return null;
        return input.Select(p => new ProductOrder(p.OrderId, p.ProductId, p.Quantity)).ToList();
    }
    public static ProductOrder? ToProductOrder(this InputCreateProductOrder? input)
    {
        if (input is null) return null;
        return new ProductOrder(input.OrderId, input.ProductId, input.Quantity);
    }
    public static List<OutputProductOrder?> ToOuputProductOrder(this List<ProductOrder?> productOrders)
    {
        if (productOrders is null) return null;
        return productOrders.Select(p => new OutputProductOrder(p.OrderId, p.ProductId, p.Quantity)).ToList();
    }
    public static OutputProductOrder? ToOuputProductOrder(this ProductOrder? productOrders)
    {
        if (productOrders is null) return null;
        return new OutputProductOrder(productOrders.OrderId, productOrders.ProductId, productOrders.Quantity);
    }
}
