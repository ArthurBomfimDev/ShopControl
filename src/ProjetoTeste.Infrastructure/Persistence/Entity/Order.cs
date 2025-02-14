using ProjetoTeste.Arguments.Arguments;
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

    public Order(long clientId, DateTime orderDate, List<ProductOrder> listProductOrder)
    {
        CustomerId = clientId;
        OrderDate = orderDate;
        ListProductOrder = listProductOrder;
    }

    #region Implicit Conversor
    public static implicit operator Order(OrderDTO orderDTO)
    {
        return orderDTO != null ? new Order
        {
            Id = orderDTO.Id,
            CustomerId = orderDTO.CustomerId,
            Total = orderDTO.Total,
            OrderDate = orderDTO.OrderDate,
            Customer = orderDTO.Customer,
            ListProductOrder = orderDTO.ListProductOrder != null ? orderDTO.ListProductOrder.Select(i => (ProductOrder)i).ToList() : null,
        } : null;
    }

    public static implicit operator OrderDTO(Order order)
    {
        return order != null ? new OrderDTO
        (
            order.Id,
            order.CustomerId,
            order.Total,
            order.OrderDate,
            order.Customer,
            order.ListProductOrder != null ? order.ListProductOrder.Select(i => (ProductOrderDTO)i).ToList() : null
        ) : null;
    }
    #endregion
}