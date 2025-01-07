using System.Text.Json.Serialization;
namespace ProjetoTeste.Arguments.Arguments.Brand;

[method: JsonConstructor]
public class InputUpdateBrand(string name, string code, string description)
{
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
}