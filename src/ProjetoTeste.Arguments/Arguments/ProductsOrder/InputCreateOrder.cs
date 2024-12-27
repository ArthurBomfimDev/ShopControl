using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjetoTeste.Arguments.Arguments.ProductsOrder;

[method: JsonConstructor]
public class InputCreateProductOrder(long orderId, long productId, int quantity)
{
    public long OrderId { get; private set; } = orderId;
    public long ProductId { get; private set; } = productId;
    public int Quantity { get; private set; } = quantity;
}