using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

[Table("pedido_de_produto")]
public class ProductOrder : BaseEntity
{
    [Required]
    [ForeignKey(nameof(Order))]
    [Column("id_de_pedido")]
    public long OrderId { get; set; }

    [Required]
    [ForeignKey(nameof(Product))]
    [Column("id_de_produto")]
    public long ProductId { get; set; }

    [Required]
    [Column("quantidade")]
    public int Quantity { get; set; }

    [Required]
    [Column("preco_unitario")]
    public decimal UnitPrice { get; set; }

    [Required]
    [Column("subtotal")]
    public decimal SubTotal { get; set; }

    [NotMapped]
    public virtual Order? Order { get; set; }
    [NotMapped]
    public virtual Product? Product { get; set; }

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

    #region Implict Conversor
    public static implicit operator ProductOrder(ProductOrderDTO productOrderDTO)
    {
        return productOrderDTO != null ? new ProductOrder
        {
            Id = productOrderDTO.Id,
            OrderId = productOrderDTO.OrderId,
            ProductId = productOrderDTO.ProductId,
            Quantity = productOrderDTO.Quantity,
            UnitPrice = productOrderDTO.UnitPrice,
            SubTotal = productOrderDTO.SubTotal,
            Order = productOrderDTO.Order,
            Product = productOrderDTO.Product
        } : null;
    }

    public static implicit operator ProductOrderDTO(ProductOrder productOrder)
    {
        return productOrder != null ? new ProductOrderDTO
        (
            productOrder.Id,
            productOrder.OrderId,
            productOrder.ProductId,
            productOrder.Quantity,
            productOrder.UnitPrice,
            productOrder.SubTotal,
            productOrder.Order,
            productOrder.Product
        ) : null;
    }
    #endregion
}