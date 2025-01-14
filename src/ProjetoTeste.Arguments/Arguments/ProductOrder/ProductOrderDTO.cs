namespace ProjetoTeste.Arguments.Arguments;

public class ProductOrderDTO
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal { get; set; }

    public OrderDTO? Order { get; set; }
    public ProductDTO? Product { get; set; }
}
