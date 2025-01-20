using ProjetoTeste.Arguments.Arguments.Brand;

namespace ProjetoTeste.Arguments.Arguments;

public class BrandValidate : BaseValidate
{
    public InputCreateBrand InputCreate { get; private set; }
    public InputCreateBrand RepeatedInputCreate { get; private set; }
    public List<InputIdentityUpdateBrand>? ListRepeatedInputIdetityUpdate { get; private set; }
    public BrandDTO? OriginalBrandDTO { get; private set; }
    public InputIdentityUpdateBrand InputUpdate { get; private set; }
    public InputIdentityUpdateBrand RepetedInputUpdate { get; private set; }
    public InputIdentityUpdateBrand RepetedCode { get; private set; }
    public BrandDTO? Code { get; private set; }
    public InputIdentifyDeleteBrand InputIdentifyDeleteBrand { get; private set; }
    public InputIdentifyDeleteBrand? RepeteInputDelete { get; private set; }
    public long? BrandWithProduct { get; private set; }

    public BrandValidate()
    {

    }

    public BrandValidate ValidateCreate(InputCreateBrand? inputCreate, InputCreateBrand repeatedInputCreate, BrandDTO? originalBrand)
    {
        InputCreate = inputCreate;
        RepeatedInputCreate = repeatedInputCreate;
        OriginalBrandDTO = originalBrand;
        return this;
    }
    public BrandValidate ValidateUpdate(InputIdentityUpdateBrand? inputUpdate, InputIdentityUpdateBrand repetedInputUpdate, BrandDTO? originalBrand, InputIdentityUpdateBrand repetedCode, BrandDTO? code)
    {
        InputUpdate = inputUpdate;
        RepetedInputUpdate = repetedInputUpdate;
        OriginalBrandDTO = originalBrand;
        RepetedCode = repetedCode;
        Code = code;
        return this;
    }
    public BrandValidate ValidateDelete(InputIdentifyDeleteBrand inputIdentifyDeleteBrand, BrandDTO? originalBrand, InputIdentifyDeleteBrand repeteInputDelete, long? brandWIthProduct)
    {
        InputIdentifyDeleteBrand = inputIdentifyDeleteBrand;
        OriginalBrandDTO = originalBrand;
        RepeteInputDelete = repeteInputDelete;
        BrandWithProduct = brandWIthProduct;
        return this;
    }
}