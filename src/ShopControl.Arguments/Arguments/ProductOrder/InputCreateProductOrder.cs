using ShopControl.Arguments.Arguments.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShopControl.Arguments.Arguments.ProductOrder;

public class InputCreateProductOrder : BaseInputCreate<InputCreateProductOrder>
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long OrderId { get; private set; }
    public long ProductId { get; private set; }
    public int Quantity { get; private set; }

    public InputCreateProductOrder()
    {

    }

    [JsonConstructor]
    public InputCreateProductOrder(long orderId, long productId, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }
}