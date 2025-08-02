using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Base.Crud;

namespace ShopControl.Domain.DTO.Base;

public class BaseValidateDTO_1<TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete> : BaseValidateDTO
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
{
    public TInputCreate? InputCreate { get; set; }
    public TInputIdentityUpdate? InputIdentityUpdate { get; set; }
    public TInputIdentityDelete? InputIdentityDelete { get; set; }
    public Dictionary<string, List<int>>? DictionaryLength { get; set; }
}