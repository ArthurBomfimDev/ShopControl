using ProjetoTeste.Arguments.Arguments.ProductsOrder;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    [method: JsonConstructor]
    public class InputCreateOrder(long clientId, List<InputCreateProductOrder> productOrders)
    {
        public long ClientId { get; set; } = clientId;
        public List<InputCreateProductOrder> ProductOrders { get; set; } = productOrders;
        [JsonIgnore]
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
