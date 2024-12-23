using System.Text.Json.Serialization;
namespace ProjetoEstagioAPI.Arguments.Brands;

[method: JsonConstructor]
public class InputCreateBrand(string name, string code, string description)
{
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
}