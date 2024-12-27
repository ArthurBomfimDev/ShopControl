namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class ProductOrder : BaseEntity
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }

    public ProductOrder()
    { }
    public ProductOrder(long orderId, long productId, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }
}