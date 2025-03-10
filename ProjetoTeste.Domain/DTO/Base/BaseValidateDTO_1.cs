using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.Crud;

namespace ProjetoTeste.Domain.DTO.Base;

public class BaseValidateDTO_1<TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete> : BaseValidateDTO
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
{
    public TInputCreate? InputCreate { get; set; }
    public TInputIdentityUpdate? InputIdentityUpdate { get; set; }
    public TInputIdentityDelete? InputIdentityDelete { get; set; }
}