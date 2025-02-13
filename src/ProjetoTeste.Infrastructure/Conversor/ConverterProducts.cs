//using ProjetoTeste.Arguments.Arguments;
//using ProjetoTeste.Arguments.Arguments.Product;
//using ProjetoTeste.Infrastructure.Persistence.Entity;
//namespace ProjetoTeste.Infrastructure.Conversor;

//public static class ConverterProducts
//{
//    public static OutputProduct? ToOutputProduct(this Product? product)
//    {
//        return product == null ? null : new OutputProduct(product.Id, product.Name, product.Code, product.Description, product.Price, product.BrandId, product.Stock);
//    }

//    public static ProductDTO? ToProductDTO(this Product? product)
//    {
//        return product == null ? null : new ProductDTO(product.Id, product.Name, product.Code, product.Description, product.Price, product.BrandId, product.Stock);
//    }
//}