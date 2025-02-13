using ProjetoTeste.Arguments.Arguments.Base;
using System.Text.Json.Serialization;
namespace ProjetoTeste.Arguments.Arguments.Brand;

public class OutputBrand : BaseOutput<OutputBrand>
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public OutputBrand()
    {

    }

    [JsonConstructor]
    public OutputBrand(long id, string name, string code, string description)
    {
        Id = id;
        Name = name;
        Code = code;
        Description = description;
    }
}