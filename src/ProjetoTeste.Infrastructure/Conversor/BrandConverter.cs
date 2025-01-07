using ProjetoTeste.Arguments.Arguments.Brands;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor;

public static class BrandConverter
{
    public static OutputBrand? ToOutputBrand(this Brand brand)
    {
        return brand == null ? null : new OutputBrand(brand.Id, brand.Name, brand.Code, brand.Description);
    }

    public static Brand? ToBrand(this InputCreateBrand brand)
    {
        return brand == null ? null : new Brand(brand.Name, brand.Code, brand.Description);
    }
}