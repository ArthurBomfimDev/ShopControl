using ProjetoTeste.Arguments.Arguments.Product;

namespace ProjetoTeste.Arguments.Arguments;

public class ProductValidateDTO : BaseValidate
{
    public InputCreateProduct? InputCreateProduct { get; private set; }
    public InputIdentityUpdateProduct? InputIdentityUpdateProduct { get; private set; }
    public InputIdentityDeleteProduct? InputIdentifyDeleteProduct { get; private set; }
    public ProductDTO? Original { get; private set; }
    public string? OriginalCode { get; private set; }
    public long? RepeteIdentity { get; private set; }
    public string? RepeteCode { get; private set; }
    public long? BrandId { get; private set; }
    public long? RepetedIdentity { get; private set; }


    public ProductValidateDTO ValidateCreate(InputCreateProduct inputCreateProduct, ProductDTO original, string repeteCode, long brandId)
    {
        InputCreateProduct = inputCreateProduct;
        Original = original;
        RepeteCode = repeteCode;
        BrandId = brandId;
        return this;
    }
    public ProductValidateDTO ValidateUpdate(InputIdentityUpdateProduct? inputIdentityUpdateProduct, ProductDTO? original, string? originalCode, long? repeteIdentity, string? repeteCode, long? brandId)
    {
        InputIdentityUpdateProduct = inputIdentityUpdateProduct;
        Original = original;
        OriginalCode = originalCode;
        RepeteIdentity = repeteIdentity;
        RepeteCode = repeteCode;
        BrandId = brandId;
        return this;
    }
    public ProductValidateDTO ValidateDelete(InputIdentityDeleteProduct? inputIdentifyDeleteProduct, ProductDTO? original, long? repetedIdentity)
    {
        InputIdentifyDeleteProduct = inputIdentifyDeleteProduct;
        Original = original;
        RepetedIdentity = repetedIdentity;
        return this;
    }
}