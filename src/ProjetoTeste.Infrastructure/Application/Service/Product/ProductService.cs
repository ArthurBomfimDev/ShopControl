using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ProductValidateService _productValidateService;

    public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, ProductValidateService productValidateService)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _productValidateService = productValidateService;
    }

    public async Task<List<OutputProduct>> GetAll()
    {
        var productList = await _productRepository.GetAll();
        return (from i in productList
                select i.ToOutputProduct()).ToList();
    }

    public async Task<OutputProduct> Get(long id)
    {
        var product = await _productRepository.Get(id);
        return product.ToOutputProduct();
    }

    public async Task<List<OutputProduct>> GetListByListId(List<long> idList)
    {
        var product = await _productRepository.GetListByListId(idList);
        return (from i in product
                select i.ToOutputProduct()).ToList();
    }

    async Task<List<OutputProduct>> GetListByBrandId(long id)
    {
        var listByBrandId = await _productRepository.GetListByBrandId(id);
        return (from i in listByBrandId select i.ToOutputProduct()).ToList();
    }

    Task<BaseResponse<List<OutputProduct>>> Create(InputCreateProduct inputCreateProduct)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<List<OutputProduct>>> CreateMultiple(List<InputCreateProduct> product)
    {
        var productValidate = await _productValidateService.ValidateCreate(product);
        var response = new BaseResponse<List<OutputProduct>>() { Message = productValidate.Message };
        if (!productValidate.Success)
        {
            response.Success = false;
            return response;
        }

        var newProduct = (from i in productValidate.Content
                          select new Product(i.Name, i.Code, i.Description, i.Price, i.BrandId, i.Stock, default)).ToList();
        var createProduct = await _productRepository.Create(newProduct);
        response.Message = productValidate.Message;
        response.Content = (from i in createProduct select new OutputProduct(i.Id, i.Name, i.Code, i.Description, i.Price, i.BrandId, i.Stock)).ToList();
        return response;
    }

    Task<BaseResponse<bool>> Update(InputIdentityUpdateBrand inputIdentityUpdateProduct)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<bool>> UpdateMultiple(List<long> idList, List<InputUpdateProduct> inputUpdateList)
    {
        var validateUpdate = await _productValidateService.ValidateUpdate(idList, inputUpdateList);
        var response = new BaseResponse<bool>() { Message = validateUpdate.Message, Success = validateUpdate.Success };

        if (!validateUpdate.Success)
        {
            return response;
        }

        var updateProduct = await _productRepository.Update(validateUpdate.Content);

        foreach (var product in validateUpdate.Content)
        {
            response.AddSuccessMessage($" >>> Produto: {product.Name} com Id: {product.Id} foi atualizado com sucesso <<<");
        }
        return response;
    }

    Task<BaseResponse<bool>> Delete(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<bool>> DeleteMultiple(List<long> idList)
    {
        var deleteValidate = await _productValidateService.ValidateDelete(idList);
        var response = new BaseResponse<bool>() { Message = deleteValidate.Message };
        if (!deleteValidate.Success)
        {
            response.Success = false;
            return response;
        }

        var productList = await _productRepository.GetListByListId(idList);
        await _productRepository.Delete(productList);
        foreach (var product in productList)
        {
            response.AddSuccessMessage($" >>> Produto: {product.Name} com Id: {product.Id} foi deletado com sucesso <<<");
        }
        return response;
    }

    Task<List<OutputProduct>> IProductService.GetListByBrandId(long id)
    {
        throw new NotImplementedException();
    }

    Task<BaseResponse<List<OutputProduct>>> IProductService.Create(InputCreateProduct inputCreateProduct)
    {
        throw new NotImplementedException();
    }

    Task<BaseResponse<bool>> IProductService.Update(InputIdentityUpdateBrand inputIdentityUpdateProduct)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateBrand> inputIdentityUpdateProduct)
    {
        throw new NotImplementedException();
    }

    Task<BaseResponse<bool>> IProductService.Delete(long id)
    {
        throw new NotImplementedException();
    }
}