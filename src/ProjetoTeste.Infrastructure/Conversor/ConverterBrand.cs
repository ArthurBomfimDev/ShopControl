using ProjetoTeste.Arguments.Arguments.Brands;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Conversor;

public static class ConverterBrand
{
    public static OutputBrand? ToOutputBrand(this Brand brand)
    {
        if (brand is null) return null;
        return new OutputBrand(brand.Id, brand.Name, brand.Code, brand.Description);
    }
    public static List<OutputBrand?> ToOutputBrandList(this List<Brand> brand)
    {
        if (brand is null) return null;
        return brand.Select(b => new OutputBrand(b.Id, b.Name, b.Code, b.Description)).ToList();
    }
    public static Brand? ToBrand(this InputCreateBrand brand)
    {
        if (brand is null) return null;
        return new Brand(brand.Name, brand.Code, brand.Description);
    }
    public static Brand? ToBrand(this InputUpdateBrand brand, long id)
    {
        if (brand is null) return null;
        return new Brand(id, brand.Name, brand.Code, brand.Description);
    }
}