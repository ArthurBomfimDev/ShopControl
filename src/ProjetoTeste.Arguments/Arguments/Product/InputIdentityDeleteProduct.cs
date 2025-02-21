using ProjetoTeste.Arguments.Arguments.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class InputIdentityDeleteProduct : BaseInputIdentityDelete<InputIdentityDeleteProduct>
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long Id { get; set; }
    public InputIdentityDeleteProduct()
    {

    }

    [JsonConstructor]
    public InputIdentityDeleteProduct(long id)
    {
        Id = id;
    }
}
