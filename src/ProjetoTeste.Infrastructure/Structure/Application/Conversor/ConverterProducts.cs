using ProjetoTeste.Models;
using ProjetoTeste.Arguments.Products;
namespace ProjetoTeste.Mapping.Products;

public static class ConverterProducts
{
    public static OutputProduct? ToOutputProduct(this Product? product)
    {
        if (product is null) return null;
        return new OutputProduct(product.Id, product.Name, product.Code, product.Description, product.Price, product.BrandId, product.Stock);
    }
    public static List<OutputProduct?> ToListOutProducts(this List<Product?> product)
    {
        if (product is null) return null;
        return product.Select(p => new OutputProduct(p.Id, p.Name, p.Code, p.Description, p.Price, p.BrandId, p.Stock)).ToList();
    }
    public static Product? ToProduct(this InputCreateProduct? product)
    {
        if (product is null) return null;
        return new Product(product.Name, product.Code, product.Description,product.Price, product.BrandId, product.Stock);
    }
    public static Product? ToProduct(this InputUpdateProduct? product)
    {
        if (product is null) return null;
        return new Product(product.Name, product.Code, product.Description, product.Price, product.BrandId, product.Stock);
    }
}