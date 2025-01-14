using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor;

public static class BrandConverter
{
    public static OutputBrand? ToOutputBrand(this Brand? brand)
    {
        return brand == null ? null : new OutputBrand(brand.Id, brand.Name, brand.Code, brand.Description, brand.ListProduct == null ? null : brand.ListProduct.Select(b => new OutputProduct(b.Id, b.Name, b.Code, b.Description, b.Price, b.BrandId, b.Stock)).ToList());
    }

    public static Brand? ToBrand(this InputCreateBrand brand)
    {
        return brand == null ? null : new Brand(brand.Name, brand.Code, brand.Description, default);
    }

    public static BrandDTO? ToBrandDTO(this Brand brand)
    {
        return brand == null ? null : new BrandDTO(brand.Id, brand.Name, brand.Code, brand.Description, default);
    }
}