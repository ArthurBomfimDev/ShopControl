namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Order : BaseEntity
{

    public long ClientId { get; set; }
    public Client Client { get; set; }
    public List<ProductOrder> ProductOrders { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal Total { get; set; }

    public Order() { }

    public Order(long clientId, DateOnly orderDate)
    {
        ClientId = clientId;
        OrderDate = orderDate;
    }
}