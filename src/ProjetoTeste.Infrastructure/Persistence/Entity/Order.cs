using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Order : BaseEntity
{

    public long CustomerId { get; set; }
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }

    public Customer Customer { get; set; }
    public List<ProductOrder> ListProductOrder { get; set; }

    public Order() { }

    public Order(long clientId, DateTime orderDate)
    {
        CustomerId = clientId;
        OrderDate = orderDate;
    }
}