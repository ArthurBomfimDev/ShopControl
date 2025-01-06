using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order;

[method: JsonConstructor]
public class OutputSellProduct(long id, string name, string code, string description, decimal price, long brandId, long stock, long quantitySold)
{
    public long Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
    public decimal Price { get; private set; } = price;
    public long BrandId { get; private set; } = brandId;
    public long Stock { get; private set; } = stock;
    public long QuantitySold { get; private set; } = quantitySold;
}