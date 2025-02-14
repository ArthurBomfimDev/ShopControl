using ProjetoTeste.Arguments.Arguments.ProductOrder;

namespace ProjetoTeste.Arguments.Arguments;

public class ProductOrderValidateDTO : BaseValidate
{
    public InputCreateProductOrder InputCreateProductOrder { get; private set; }
    public long OrderId { get; private set; }
    public ProductDTO Product { get; private set; }

    public ProductOrderValidateDTO CreateValidate(InputCreateProductOrder inputCreateProductOrder, long orderid, ProductDTO product)
    {
        InputCreateProductOrder = inputCreateProductOrder;
        OrderId = orderid;
        Product = product;
        return this;
    }
}