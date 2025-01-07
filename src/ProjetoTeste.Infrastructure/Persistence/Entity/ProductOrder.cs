using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class ProductOrder : BaseEntity
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal { get; set; }

    public Order? Order { get; set; }
    public Product? Product { get; set; }

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
}