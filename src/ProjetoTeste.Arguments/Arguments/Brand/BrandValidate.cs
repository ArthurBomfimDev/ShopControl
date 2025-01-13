using ProjetoTeste.Arguments.Arguments.Brand;

namespace ProjetoTeste.Arguments.Arguments;

public class BrandValidate : BaseValidate
{
    public List<InputCreateBrand>? LisRepeatedInputCreate { get; private set; }
    public List<InputIdentityUpdateBrand>? ListRepeatedInputIdetityUpdate { get; private set; }

    public BrandValidate ValidateCreate(List<InputCreateBrand>? LisRepeatedInputCreate, )
}

