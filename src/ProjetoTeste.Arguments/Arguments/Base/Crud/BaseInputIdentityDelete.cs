using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Base;

public class BaseInputIdentityDelete<TInputIdentityDelete> where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete> 
{
    [Required(ErrorMessage = "O campo {0} é OBRIGATÓRIO - Identificador")]
    public long Id { get; set; }

    [JsonConstructor]
    public BaseInputIdentityDelete(long id)
    {
        Id = id;
    }
}
public class BaseInputIdentityDelete_0(long id) : BaseInputIdentityDelete<BaseInputIdentityDelete_0>(id) { }