using ProjetoEstagioAPI.Models;
using System.Text.Json.Serialization;

namespace ProjetoEstagioAPI.Arguments.Order
{
    [method: JsonConstructor]
    public class InputCreateOrder(long clientId, List<ProductOrder> productOrders)
    {
        public long ClientId { get; set; } = clientId;
        public List<ProductOrder> ProductOrders { get; set; } = productOrders;
        [JsonIgnore]
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
