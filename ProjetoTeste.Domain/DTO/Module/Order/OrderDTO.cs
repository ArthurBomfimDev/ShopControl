using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Domain.DTO.Base;

namespace ProjetoTeste.Domain.DTO;

public class OrderDTO : BaseDTO<OrderDTO>
{
    public long CustomerId { get; private set; }
    public decimal Total { get; private set; }
    public DateTime OrderDate { get; private set; }

    public CustomerDTO Customer { get; private set; }
    public List<ProductOrderDTO> ListProductOrder { get; private set; }

    public OrderDTO() { }

    public OrderDTO(long customerId, DateTime orderDate, List<ProductOrderDTO> listProductOrder)
    {
        CustomerId = customerId;
        OrderDate = orderDate;
        ListProductOrder = listProductOrder;
    }

    public OrderDTO(long id, long customerId, decimal total, DateTime orderDate, CustomerDTO customer, List<ProductOrderDTO> listProductOrder)
    {
        Id = id;
        CustomerId = customerId;
        Total = total;
        OrderDate = orderDate;
        Customer = customer;
        ListProductOrder = listProductOrder;
    }

    #region Implicit Conversor
    public static implicit operator OutputOrder(OrderDTO orderDTO)
    {
        return orderDTO != null ? new OutputOrder
            (
            orderDTO.Id,
            orderDTO.CustomerId,
            orderDTO.ListProductOrder != null ? orderDTO.ListProductOrder.Select(i => (OutputProductOrder)i).ToList() : null,
            orderDTO.Total,
            orderDTO.OrderDate
            ) : null;
    }
    #endregion
}