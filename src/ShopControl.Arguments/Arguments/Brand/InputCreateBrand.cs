using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.DataAnnotation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ShopControl.Arguments.Arguments.Brand;

public class InputCreateBrand : BaseInputCreate<InputCreateBrand>
{
    public string Name { get; private set; }
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    [IdentifierAttribute]
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