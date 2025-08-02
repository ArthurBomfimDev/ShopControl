using ShopControl.Arguments.Arguments.Product;
using ShopControl.Domain.DTO;
using ShopControl.Domain.DTO.Base;

namespace ShopControl.Arguments.Arguments;

public class ProductValidateDTO : BaseValidateDTO_1<InputCreateProduct, InputUpdateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct>
{
    public ProductDTO? Original { get; private set; }
    public string? OriginalCode { get; private set; }
    public long? RepeteIdentity { get; private set; }
    public string? RepeteCode { get; private set; }
    public long? BrandId { get; private set; }
    public long? RepetedIdentity { get; private set; }


    public ProductValidateDTO ValidateCreate(InputCreateProduct inputCreateProduct, string? originalCode, string repeteCode, long brandId, Dictionary<string, List<int>> dictionaryLength)
    {
        InputCreate = inputCreateProduct;
        OriginalCode = originalCode;
        RepeteCode = repeteCode;
        BrandId = brandId;
        DictionaryLength = dictionaryLength;
        return this;
    }
    public ProductValidateDTO ValidateUpdate(InputIdentityUpdateProduct? inputIdentityUpdateProduct, ProductDTO? original, string? originalCode, long? repeteIdentity, string? repeteCode, long? brandId, Dictionary<string, List<int>> dictionaryLength)
    {
        InputIdentityUpdate = inputIdentityUpdateProduct;
        Original = original;
        OriginalCode = originalCode;
        RepeteIdentity = repeteIdentity;
        RepeteCode = repeteCode;
        BrandId = brandId;
        DictionaryLength = dictionaryLength;
        return this;
    }
    public ProductValidateDTO ValidateDelete(InputIdentityDeleteProduct? inputIdentifyDeleteProduct, ProductDTO? original, long? repetedIdentity)
    {
        InputIdentityDelete = inputIdentifyDeleteProduct;
        Original = original;
        RepetedIdentity = repetedIdentity;
        return this;
    }
}