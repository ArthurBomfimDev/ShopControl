using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Brand : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    public virtual List<Product>? ListProduct { get; set; }

    public Brand()
    { }

    public Brand(string name, string code, string description, List<Product>? listProduct)
    {
        Name = name;
        Code = code;
        Description = description;
        ListProduct = listProduct;
    }

    #region Implicit Operator
    public static implicit operator Brand(BrandDTO brandDTO)
    {
        return brandDTO != null ? new Brand
        {
            Id = brandDTO.Id,
            Name = brandDTO.Name,
            Code = brandDTO.Code,
            Description = brandDTO.Description,
            ListProduct = brandDTO.ListProduct.Select(i => (Product)i).ToList()
        } : null;
    }

    public static implicit operator BrandDTO(Brand brand)
    {
        return brand != null ? new BrandDTO
        (
            brand.Id,
            brand.Name,
            brand.Code,
            brand.Description,
            brand.ListProduct.Select(i => (ProductDTO)i).ToList()
        ) : null;
    }
    #endregion
}