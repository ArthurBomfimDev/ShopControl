using ProjetoTeste.Arguments.Arguments.Products;
using ProjetoTeste.Infrastructure.Persistence.Entity;
namespace ProjetoTeste.Infrastructure.Conversor;

public static class ConverterProducts
{
    public static OutputProduct? ToOutputProduct(this Product? product)
    {
        return product == null ? null : new OutputProduct(product.Id, product.Name, product.Code, product.Description, product.Price, product.BrandId, product.Stock);
    }

    public static Product? ToProduct(this InputCreateProduct? product)
    {
        return product == null ? null : new Product(product.Name, product.Code, product.Description, product.Price, product.BrandId, product.Stock);
    }

    public static Product? ToProduct(this InputUpdateProduct? product)
    {
        return product == null ? null : new Product(product.Name, product.Code, product.Description, product.Price, product.BrandId, product.Stock);
    }
}