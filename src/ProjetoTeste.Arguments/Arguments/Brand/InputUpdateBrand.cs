using System.Text.Json.Serialization;
namespace ProjetoTeste.Arguments.Arguments.Brand;

public class InputUpdateBrand
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public InputUpdateBrand()
    {

    }

    [JsonConstructor]
    public InputUpdateBrand(string name, string code, string description)
    {
        Name = name;
        Code = code;
        Description = description;
    }
}