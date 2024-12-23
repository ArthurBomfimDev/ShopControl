namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class ProductOrder : BaseEntity
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }

    public ProductOrder()
    { }
    public ProductOrder(long productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}