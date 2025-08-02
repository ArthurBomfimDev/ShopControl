using ShopControl.Arguments.Arguments.Brand;
using ShopControl.Domain.DTO;
using ShopControl.Domain.DTO.Base;

namespace ShopControl.Arguments.Arguments;

public class BrandValidateDTO : BaseValidateDTO_1<InputCreateBrand, InputUpdateBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand>
{
    public string? RepeatedInputCreateCode { get; private set; }
    public List<InputIdentityUpdateBrand>? ListRepeatedInputIdetityUpdate { get; private set; }
    public BrandDTO? OriginalBrandDTO { get; private set; }
    public long RepetedInputUpdateIdentity { get; private set; }
    public string RepetedCode { get; private set; }
    public string? CodeExists { get; private set; }
    public InputIdentityDeleteBrand? RepeteInputDelete { get; private set; }
    public long? BrandWithProduct { get; private set; }

    public BrandValidateDTO ValidateCreate(InputCreateBrand? inputCreate, string? repeatedInputCreate, BrandDTO? originalBrand, Dictionary<string, List<int>>? dictionaryLength)
    {
        InputCreate = inputCreate;
        RepeatedInputCreateCode = repeatedInputCreate;
        OriginalBrandDTO = originalBrand;
        DictionaryLength = dictionaryLength;
        return this;
    }
    public BrandValidateDTO ValidateUpdate(InputIdentityUpdateBrand? inputUpdate, long repetedInputUpdate, BrandDTO? originalBrand, string repetedCode, string? codeExists, Dictionary<string, List<int>>? dictionaryLength)
    {
        InputIdentityUpdate = inputUpdate;
        RepetedInputUpdateIdentity = repetedInputUpdate;
        OriginalBrandDTO = originalBrand;
        RepetedCode = repetedCode;
        CodeExists = codeExists;
        DictionaryLength = dictionaryLength;
        return this;
    }
    public BrandValidateDTO ValidateDelete(InputIdentityDeleteBrand inputIdentifyDeleteBrand, BrandDTO? originalBrand, InputIdentityDeleteBrand repeteInputDelete, long? brandWIthProduct)
    {
        InputIdentityDelete = inputIdentifyDeleteBrand;
        OriginalBrandDTO = originalBrand;
        RepeteInputDelete = repeteInputDelete;
        BrandWithProduct = brandWIthProduct;
        return this;
    }
}