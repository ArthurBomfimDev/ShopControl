using ProjetoTeste.Arguments.Arguments.Brand;

namespace ProjetoTeste.Arguments.Arguments;

public class BrandValidate : BaseValidate
{
    public InputCreateBrand InputCreate { get; private set; }
    public string? RepeatedInputCreateCode { get; private set; }
    public List<InputIdentityUpdateBrand>? ListRepeatedInputIdetityUpdate { get; private set; }
    public BrandDTO? OriginalBrandDTO { get; private set; }
    public InputIdentityUpdateBrand InputUpdate { get; private set; }
    public long RepetedInputUpdateIdentify { get; private set; }
    public string RepetedCode { get; private set; }
    public string? CodeExists { get; private set; }
    public InputIdentifyDeleteBrand InputIdentifyDeleteBrand { get; private set; }
    public InputIdentifyDeleteBrand? RepeteInputDelete { get; private set; }
    public long? BrandWithProduct { get; private set; }

    public BrandValidate()
    {

    }

    public BrandValidate ValidateCreate(InputCreateBrand? inputCreate, string? repeatedInputCreate, BrandDTO? originalBrand)
    {
        InputCreate = inputCreate;
        RepeatedInputCreateCode = repeatedInputCreate;
        OriginalBrandDTO = originalBrand;
        return this;
    }
    public BrandValidate ValidateUpdate(InputIdentityUpdateBrand? inputUpdate, long repetedInputUpdate, BrandDTO? originalBrand, string repetedCode, string? codeExists)
    {
        InputUpdate = inputUpdate;
        RepetedInputUpdateIdentify = repetedInputUpdate;
        OriginalBrandDTO = originalBrand;
        RepetedCode = repetedCode;
        CodeExists = codeExists;
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