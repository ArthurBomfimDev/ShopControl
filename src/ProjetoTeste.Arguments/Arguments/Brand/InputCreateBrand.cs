using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;
namespace ProjetoTeste.Arguments.Arguments.Brand;

public class InputCreateBrand : BaseInputCreate<InputCreateBrand>
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public InputCreateBrand()
    {

    }

    [JsonConstructor]
    public InputCreateBrand(string name, string code, string description)
    {
        Name = name;
        Code = code;
        Description = description;
    }
}