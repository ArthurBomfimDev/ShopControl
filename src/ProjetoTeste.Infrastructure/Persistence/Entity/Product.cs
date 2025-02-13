using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;
public class Product : BaseEntity
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public long BrandId { get; set; }
    public long Stock { get; set; }

    public virtual Brand? Brand { get; set; }
    public virtual List<ProductOrder>? ListProductOrder { get; set; }

    public Product()
    { }

    public Product(string name, string code, string description, decimal price, long brandId, long stock, List<ProductOrder>? listProductOrder)
    {
        Name = name;
        Code = code;
        Description = description;
        Price = price;
        BrandId = brandId;
        Stock = stock;
        ListProductOrder = listProductOrder;
    }

    #region Implicit Operator
    public static implicit operator Product(ProductDTO productDTO) => productDTO != null ? new Product
    {
        Id = productDTO.Id,
        Name = productDTO.Name,
        Code = productDTO.Code,
        Description = productDTO.Description,
        Price = productDTO.Price,
        BrandId = productDTO.BrandId,
        Stock = productDTO.Stock,
        Brand = productDTO.Brand,
        ListProductOrder = productDTO.ListProductOrder.Select(i => (ProductOrder)i).ToList()
    } : null;

    public static implicit operator ProductDTO(Product product)
    {
        return product == null ? null : new ProductDTO
        (
            product.Id,
            product.Name,
            product.Code,
            product.Description,
            product.Price,
            product.BrandId,
            product.Stock,
            product.Brand,
            product.ListProductOrder.Select(i => (ProductOrderDTO)i).ToList()
        );
    }
    #endregion
}