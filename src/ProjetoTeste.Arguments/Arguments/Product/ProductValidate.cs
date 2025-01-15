using ProjetoTeste.Arguments.Arguments.Product;

namespace ProjetoTeste.Arguments.Arguments;

public class ProductValidate : BaseValidate
{
    public InputCreateProduct? InputCreateProduct { get; private set; }
    public InputIdentityUpdateBrand? InputIdentityUpdateBrand { get; private set; }
    public ProductDTO? Original { get; private set; }
    public ProductDTO? OriginalCode { get; private set; }
    public long? RepeteIdentity { get; private set; }
    public string? RepeteCode { get; private set; }
    public long? BrandId { get; private set; }


    public ProductValidate ValidateCreate(InputCreateProduct inputCreateProduct, ProductDTO original, string repeteCode, long brandId)
    {
        InputCreateProduct = inputCreateProduct;
        Original = original;
        RepeteCode = repeteCode;
        BrandId = brandId;
        return this;
    }
    public ProductValidate ValidateUpdate(InputIdentityUpdateBrand? inputIdentityUpdateBrand, ProductDTO? original, ProductDTO? originalCode, long? repeteIdentity, string? repeteCode, long? brandId)
    {
        InputIdentityUpdateBrand = inputIdentityUpdateBrand;
        Original = original;
        OriginalCode = originalCode;
        RepeteIdentity = repeteIdentity;
        RepeteCode = repeteCode;
        BrandId = brandId;
        return this;
    }
}
