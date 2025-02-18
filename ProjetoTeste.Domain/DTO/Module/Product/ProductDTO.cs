using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Domain.DTO.Base;

namespace ProjetoTeste.Arguments.Arguments;

public class ProductDTO : BaseDTO<ProductDTO>
{
    public ProductDTO()
    {
    }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long BrandId { get; set; }
    public long Stock { get; set; }

    public virtual BrandDTO? Brand { get; set; }
    public virtual List<ProductOrderDTO>? ListProductOrder { get; set; }

    public ProductDTO(long id, string name, string code, string description, decimal price, long brandId, long stock, BrandDTO? brand, List<ProductOrderDTO>? listProductOrder)
    {
        Id = id;
        Name = name;
        Code = code;
        Description = description;
        Price = price;
        BrandId = brandId;
        Stock = stock;
        Brand = brand;
        ListProductOrder = listProductOrder;
    }

    #region Implicit Conversor
    public static implicit operator OutputProduct(ProductDTO productDTO)
    {
        return productDTO != null ? new OutputProduct
            (
                productDTO.Id,
                productDTO.Name,
                productDTO.Code,
                productDTO.Description,
                productDTO.Price,
                productDTO.BrandId,
                productDTO.Stock
            ) : null;
    }
    #endregion
}