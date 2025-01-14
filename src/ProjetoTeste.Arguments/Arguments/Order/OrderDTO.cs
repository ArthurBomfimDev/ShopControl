namespace ProjetoTeste.Arguments.Arguments;

public class OrderDTO
{
    public long CustomerId { get; set; }
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }

    public CustomerDTO Customer { get; set; }
    public List<ProductOrderDTO> ListProductOrder { get; set; }
}