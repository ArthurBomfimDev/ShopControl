using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;
public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long BrandId { get; set; }
    public long Stock { get; set; }

    public Brand? Brand { get; set; }
    public List<ProductOrder>? ListProductOrder { get; set; }

    public Product()
    { }

    public Product(string name, string code, string description, decimal price, long brandId, long stock)
    {
        Name = name;
        Code = code;
        Description = description;
        Price = price;
        BrandId = brandId;
        Stock = stock;
    }
}