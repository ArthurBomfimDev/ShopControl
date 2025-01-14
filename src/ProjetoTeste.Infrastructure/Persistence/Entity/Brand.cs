using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Brand : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    public List<Product>? ListProduct { get; set; }

    public Brand()
    { }

    public Brand(string name, string code, string description, List<Product>? listProduct)
    {
        Name = name;
        Code = code;
        Description = description;
        ListProduct = listProduct;
    }

    public Brand(long id, string name, string code, string description, List<Product>? listProduct)
    {
        Id = id;
        Name = name;
        Code = code;
        Description = description;
        ListProduct = listProduct;
    }

}