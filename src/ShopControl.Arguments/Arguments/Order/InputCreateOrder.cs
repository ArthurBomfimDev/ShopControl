using ShopControl.Arguments.Arguments.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShopControl.Arguments.Arguments.Order
{
    public class InputCreateOrder : BaseInputCreate<InputCreateOrder>
    {
        [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
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