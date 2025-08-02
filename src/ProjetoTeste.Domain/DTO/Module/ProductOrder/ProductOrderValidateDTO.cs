using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Domain.DTO;

namespace ProjetoTeste.Arguments.Arguments;

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