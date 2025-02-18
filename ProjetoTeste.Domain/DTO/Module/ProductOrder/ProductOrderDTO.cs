using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Domain.DTO.Base;

namespace ProjetoTeste.Arguments.Arguments;

public class ProductOrderDTO : BaseDTO<ProductOrderDTO>
{
    public long OrderId { get; private set; }
    public long ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal SubTotal { get; private set; }

    public virtual OrderDTO? Order { get; private set; }
    public virtual ProductDTO? Product { get; private set; }

    public ProductOrderDTO()
    {

    }

    public ProductOrderDTO(long id, long orderId, long productId, int quantity, decimal unitPrice, decimal subTotal, OrderDTO? order, ProductDTO? product)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        SubTotal = subTotal;
        Order = order;
        Product = product;
    }

    #region Implicit Conversor
    public static implicit operator OutputProductOrder(ProductOrderDTO productOrderDTO)
    {
        return productOrderDTO != null ? new OutputProductOrder
            (
            productOrderDTO.Id,
            productOrderDTO.OrderId,
            productOrderDTO.ProductId,
            productOrderDTO.Quantity,
            productOrderDTO.UnitPrice,
            productOrderDTO.SubTotal
            ) : null;
    }
    #endregion
}