using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

[Table("pedido")]
public class Order : BaseEntity
{
    [Required]
    [Column("id_do_cliente")]
    [ForeignKey(nameof(Customer))]
    public long CustomerId { get; set; }

    [Required]
    [Column("total")]
    public decimal Total { get; set; }

    [Required]
    [Column("data_do_pedido")]
    public DateTime OrderDate { get; set; }

    [NotMapped]
    public Customer? Customer { get; set; }

    [NotMapped]
    public List<ProductOrder>? ListProductOrder { get; set; }

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