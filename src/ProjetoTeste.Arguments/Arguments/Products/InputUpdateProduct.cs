using System.Text.Json.Serialization;
namespace ProjetoEstagioAPI.Arguments.Products;

[method: JsonConstructor]
public class InputUpdateProduct(string name, string code, string description, decimal price, long brandId, long stock)
{
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
    public decimal Price { get; private set; } = price;
    public long BrandId { get; private set; } = brandId;
    public long Stock { get; private set; } = stock;
}