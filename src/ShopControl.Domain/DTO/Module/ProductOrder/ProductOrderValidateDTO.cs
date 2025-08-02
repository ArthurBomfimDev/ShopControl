using ShopControl.Arguments.Arguments.ProductOrder;
using ShopControl.Domain.DTO;

namespace ShopControl.Arguments.Arguments;

public class ProductOrderValidateDTO : BaseValidateDTO
{
    public InputCreateProductOrder InputCreateProductOrder { get; private set; }
    public OrderDTO OrderDTO { get; private set; }
    public ProductDTO Product { get; private set; }

    public ProductOrderValidateDTO CreateValidate(InputCreateProductOrder inputCreateProductOrder, OrderDTO orderDTO, ProductDTO product)
    {
        InputCreateProductOrder = inputCreateProductOrder;
        OrderDTO = orderDTO;
        Product = product;
        return this;
    }
}