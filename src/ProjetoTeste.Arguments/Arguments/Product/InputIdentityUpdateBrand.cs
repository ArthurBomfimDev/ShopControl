using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityUpdateProduct : BaseInputIdentityUpdate<InputIdentityUpdateProduct>
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long Id { get; private set; }
    public InputUpdateProduct InputUpdateProduct { get; private set; }

    public InputIdentityUpdateProduct()
    {

    }

    [JsonConstructor]
    public InputIdentityUpdateProduct(long id, InputUpdateProduct inputUpdateProduct)
    {
        Id = id;
        InputUpdateProduct = inputUpdateProduct;
    }
}