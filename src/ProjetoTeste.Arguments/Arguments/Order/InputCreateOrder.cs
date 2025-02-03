using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    [method: JsonConstructor]
    public class InputCreateOrder(long customerId) : BaseInputCreate<InputCreateOrder>
    {
        public long CustomerId { get; set; } = customerId;
    }
}