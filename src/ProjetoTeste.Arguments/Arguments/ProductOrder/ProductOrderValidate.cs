using ProjetoTeste.Arguments.Arguments.ProductOrder;

namespace ProjetoTeste.Arguments.Arguments;

public class ProductOrderValidate : BaseValidateDTO
{
    public InputCreateProductOrder InputCreateProductOrder { get; private set; }
    public long OrderId { get; private set; }
    public ProductDTO Product { get; private set; }

    public ProductOrderValidate CreateValidate(InputCreateProductOrder inputCreateProductOrder, long orderid, ProductDTO product)
    {
        InputCreateProductOrder = inputCreateProductOrder;
        OrderId = orderid;
        Product = product;
        return this;
    }
}