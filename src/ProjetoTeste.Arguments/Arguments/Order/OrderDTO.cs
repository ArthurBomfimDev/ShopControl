namespace ProjetoTeste.Arguments.Arguments;

public class OrderDTO
{
    public long Id { get; private set; }
    public long CustomerId { get; private set; }
    public decimal Total { get; private set; }
    public DateTime OrderDate { get; private set; }

    public CustomerDTO Customer { get; private set; }
    public List<ProductOrderDTO> ListProductOrder { get; private set; }

    public OrderDTO(long id, long customerId, decimal total, DateTime orderDate, List<ProductOrderDTO> listProductOrder)
    {
        Id = id;
        CustomerId = customerId;
        Total = total;
        OrderDate = orderDate;
        ListProductOrder = listProductOrder;
    }
}