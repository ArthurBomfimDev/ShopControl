using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

public class Brand : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Description { get; set; }

    public virtual List<Product>? ListProduct { get; set; }

    //Dictionary<string, List<int>> dict = (from i in typeof(Brand).GetProperties()
    //                                      where i.PropertyType == typeof(string)
    //                                      select new
    //                                      {
    //                                          PropertyName = i.Name,
    //                                          MaxLength = i.GetCustomAttribute<MaxLengthAttribute>()?.Length ?? 0,
    //                                          MinLength = i.GetCustomAttribute<MinLengthAttribute>()?.Length ?? 0
    //                                      }).ToDictionary(j => j.PropertyName, j => new List<int> { j.MinLength, j.MaxLength });

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
        if (brandDTO == null)
            return null;

        return new Brand
        {
            Id = brandDTO.Id,
            Name = brandDTO.Name,
            Code = brandDTO.Code,
            Description = brandDTO.Description,
            ListProduct = brandDTO.ListProduct != null ? brandDTO.ListProduct.Select(i => (Product)i).ToList() : null
        };
    }

    public static implicit operator BrandDTO(Brand brand)
    {
        return brand != null ? new BrandDTO
        (
            brand.Id,
            brand.Name,
            brand.Code,
            brand.Description,
            brand.ListProduct != null ? brand.ListProduct.Select(i => (ProductDTO)i).ToList() : null
        ) : null;
    }
    #endregion
}