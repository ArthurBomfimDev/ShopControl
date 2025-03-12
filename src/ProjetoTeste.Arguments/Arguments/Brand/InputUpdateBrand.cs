using ProjetoTeste.Arguments.Arguments.Base.Crud;
using ProjetoTeste.Arguments.DataAnnotation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ProjetoTeste.Arguments.Arguments.Brand;

public class InputUpdateBrand : BaseInputUpdate<InputUpdateBrand>
{
    public string Name { get; private set; }

    [IdentifierAttribute]
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
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