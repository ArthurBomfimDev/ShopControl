using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    public class InputCreateOrder : BaseInputCreate<InputCreateOrder>
    {
        public long CustomerId { get; set; }

        public InputCreateOrder()
        {

        }

        [JsonConstructor]
        public InputCreateOrder(long customerId)
        {
            CustomerId = customerId;
        }
    }
}