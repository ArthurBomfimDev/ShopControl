using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;
namespace ProjetoTeste.Arguments.Arguments.Product;

public class InputCreateProduct : BaseInputCreate<InputCreateProduct>
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public long BrandId { get; private set; }
    public long Stock { get; private set; }

    public InputCreateProduct()
    {
        
    }

    [JsonConstructor]
    public InputCreateProduct(string name, string code, string description, decimal price, long brandId, long stock)
    {
        Name = name;
        Code = code;
        Description = description;
        Price = price;
        BrandId = brandId;
        Stock = stock;
    }
}