using ShopControl.Arguments.Arguments.Base.Crud;
using ShopControl.Arguments.DataAnnotation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ShopControl.Arguments.Arguments.Product;

public class InputUpdateProduct : BaseInputUpdate<InputUpdateProduct>
{
    public string Name { get; private set; }
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    [IdentifierAttribute]
    public string Code { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public long BrandId { get; private set; }
    public long Stock { get; private set; }

    public InputUpdateProduct()
    {

    }

    [JsonConstructor]
    public InputUpdateProduct(string name, string code, string description, decimal price, long brandId, long stock)
    {
        Name = name;
        Code = code;
        Description = description;
        Price = price;
        BrandId = brandId;
        Stock = stock;
    }
}