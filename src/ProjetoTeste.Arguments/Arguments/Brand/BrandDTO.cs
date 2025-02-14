using ProjetoTeste.Arguments.Arguments.Brand;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments;

public class BrandDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public List<ProductDTO>? ListProduct { get; set; }

    public BrandDTO() { }

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