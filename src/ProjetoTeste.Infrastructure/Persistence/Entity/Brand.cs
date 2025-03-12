using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;

[Table("marca")]
public class Brand : BaseEntity
{
    [Required]
    [Column("nome")]
    [MaxLength(40)]
    public string Name { get; set; }

    [Required]
    [Column("codigo")]
    [MaxLength(6)]
    public string Code { get; set; }

    [MaxLength(100)]
    [Column("descricao")]
    public string? Description { get; set; }

    [NotMapped]
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