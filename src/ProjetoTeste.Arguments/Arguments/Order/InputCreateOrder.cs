using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    [method: JsonConstructor]
    public class InputCreateOrder(long clientId)
    {
        public long ClientId { get; set; } = clientId;
    }
}
