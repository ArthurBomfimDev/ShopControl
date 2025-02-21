using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Domain.DTO.Base;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Domain.DTO;

public class BrandDTO : BaseDTO<BrandDTO>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public List<ProductDTO>? ListProduct { get; set; }

    public BrandDTO() { }

    public BrandDTO(string name, string code, string description, List<ProductDTO>? listProduct)
    {
        Name = name;
        Code = code;
        Description = description;
        ListProduct = listProduct;
    }

    [JsonConstructor]
    public BrandDTO(long id, string name, string code, string description, List<ProductDTO>? listProduct)
    {
        Id = id;
        Name = name;
        Code = code;
        Description = description;
        ListProduct = listProduct;
    }

    #region Implicit Operator
    public static implicit operator OutputBrand(BrandDTO brandDTO)
    {
        return brandDTO != null ? new OutputBrand(
            brandDTO.Id,
            brandDTO.Name,
            brandDTO.Code,
            brandDTO.Description) : null;
    }
    #endregion
}