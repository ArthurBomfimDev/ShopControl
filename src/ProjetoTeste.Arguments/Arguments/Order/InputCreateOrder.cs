using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    [method: JsonConstructor]
    public class InputCreateOrder(long customerId)
    {
        public long CustomerId { get; set; } = customerId;
    }
}