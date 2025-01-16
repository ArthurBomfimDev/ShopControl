namespace ProjetoTeste.Arguments.Arguments;

public class ProductOrderDTO
{
    public long OrderId { get; private set; }
    public long ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal SubTotal { get; private set; }

    public OrderDTO? Order { get; private set; }
    public ProductDTO? Product { get; private set; }

    public ProductOrderDTO(long orderId, long productId, int quantity, decimal unitPrice, decimal subTotal)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        SubTotal = subTotal;
    }
}