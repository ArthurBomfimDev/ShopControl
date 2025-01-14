namespace ProjetoTeste.Arguments.Arguments;

public class ProductDTO
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long BrandId { get; set; }
    public long Stock { get; set; }

    public BrandDTO? Brand { get; set; }
    public List<ProductOrderDTO>? ListProductOrder { get; set; }
}
