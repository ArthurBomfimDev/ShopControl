using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTeste.Infrastructure.Persistence.Entity;
[Table("produto")]
public class Product : BaseEntity
{
    [Required]
    [Column("nome")]
    [MaxLength(40)]
    public string Name { get; set; }

    [Required]
    [Column("codigo")]
    [MaxLength(6)]
    public string? Code { get; set; }

    [Column("descricao")]
    [MaxLength(100)]
    public string? Description { get; set; }

    [Required]
    [Column("preco")]
    public decimal Price { get; set; }

    [Required]
    [Column("id_da_marca")]
    [ForeignKey(nameof(Brand))]
    public long BrandId { get; set; }

    [Required]
    [Column("estoque")]
    public long Stock { get; set; }

    [NotMapped]
    public virtual Brand? Brand { get; set; }
    [NotMapped]
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
        ListProductOrder = productDTO.ListProductOrder != null ? productDTO.ListProductOrder.Select(i => (ProductOrder)i).ToList() : null
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
            product.ListProductOrder != null ? product.ListProductOrder.Select(i => (ProductOrderDTO)i).ToList() : null
        );
    }
    #endregion
}