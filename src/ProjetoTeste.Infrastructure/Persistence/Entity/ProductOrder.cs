using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class ProductOrder : BaseEntity
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal { get; set; }

    public virtual Order? Order { get; set; }
    public virtual Product? Product { get; set; }

    public ProductOrder()
    { }
    public ProductOrder(long orderId, long productId, int quantity, decimal unitPrice, decimal subTotal)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        SubTotal = subTotal;
    }

    #region Implict Conversor
    public static implicit operator ProductOrder(ProductOrderDTO productOrderDTO)
    {
        return productOrderDTO.Order != null ? new ProductOrder
        {
            Id = productOrderDTO.Id,
            OrderId = productOrderDTO.OrderId,
            ProductId = productOrderDTO.ProductId,
            Quantity = productOrderDTO.Quantity,
            UnitPrice = productOrderDTO.UnitPrice,
            SubTotal = productOrderDTO.SubTotal,
            Order = productOrderDTO.Order,
            Product = productOrderDTO.Product
        } : null;
    }

    public static implicit operator ProductOrderDTO(ProductOrder productOrder)
    {
        return productOrder.Order != null ? new ProductOrderDTO
        (
            productOrder.Id,
            productOrder.OrderId,
            productOrder.ProductId,
            productOrder.Quantity,
            productOrder.UnitPrice,
            productOrder.SubTotal,
            productOrder.Order,
            productOrder.Product
        ) : null;
    }
    #endregion
}