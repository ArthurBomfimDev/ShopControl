namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Order : BaseEntity
{

    public long CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<ProductOrder> ListProductOrder { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal Total { get; set; }

    public Order() { }

    public Order(long clientId, DateOnly orderDate)
    {
        CustomerId = clientId;
        OrderDate = orderDate;
    }
}