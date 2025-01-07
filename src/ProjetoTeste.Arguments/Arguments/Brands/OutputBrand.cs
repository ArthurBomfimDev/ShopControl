using ProjetoTeste.Arguments.Arguments.Products;
using System.Text.Json.Serialization;
namespace ProjetoTeste.Arguments.Arguments.Brands;

[method: JsonConstructor]
public class OutputBrand(long id, string name, string code, string description, List<OutputProduct> listProduct)
{
    public long Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
    public List<OutputProduct> ListProduct { get; private set; } = listProduct;
}