using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Application.Service.Product;
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

    public async Task<BaseResponse<Product>> ValidationId(List<long> id)
    {
        var productExist = await _productRepository.Get(id);
        if (productExist is null)
        {
            return new BaseResponse<Product>() { Success = false, Message = new List<Notification> { new Notification { Message = " >>> Produto com o Id digitado NÃO encontrado <<<", Type = EnumNotificationType.Error } } };
        }
        return new BaseResponse<Product>() { Success = true/*, Content = productExist */};
    }

    public async Task<BaseResponse<List<OutputProduct>>> GetAll()
    {
        var productList = await _productRepository.GetAllAsync();
        return new BaseResponse<List<OutputProduct>>
        {
            Success = true,
            Content = (from i in productList
                       select i.ToOutputProduct()).ToList()
        };
    }

    public async Task<BaseResponse<List<OutputProduct>>> Get(List<long> idList)
    {
        var product = await _productRepository.Get(idList);
        return new BaseResponse<List<OutputProduct>>
        {
            Success = true,
            Content = (from i in product
                       select i.ToOutputProduct()).ToList()
        };
    }

    public async Task<BaseResponse<List<OutputProduct>>> Create(List<InputCreateProduct> product)
    {
        var productValidate = await _productValidateService.ValidateCreate(product);
        var response = new BaseResponse<List<OutputProduct>>();
        if (!productValidate.Success)
        {
            response.Message = productValidate.Message;
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

    public async Task<BaseResponse<bool>> Update(List<long> idList, List<InputUpdateProduct> input)
    {
        //var response = new BaseResponse<bool>();
        //var idExists = await ValidationId(id);
        //var product = idExists.Content;
        //if (!idExists.Success)
        //{
        //    response.Message.Add(idExists.Message[0]);
        //}
        //if (input is null)
        //{
        //    response.Success = false;
        //    response.Message.Add(">>> Dados Inseridos Incompletos ou Inexistentes <<<");
        //}
        //bool codeExists = await _productRepository.Exist(input.Code);
        //if (input.Code != product.Code && codeExists)
        //{
        //    response.Success = false;
        //    response.Message.Add(" >>> Já existe um Produto Cadastrado com esse Código <<<");
        //}
        //if (input.Stock < 0)
        //{
        //    response.Success = false;
        //    response.Message.Add(" >>> Não é possivél criar Produto Com Stock Negativo <<<");
        //}
        //if (input.Price < 0)
        //{
        //    response.Success = false;
        //    response.Message.Add(" >>> Não é possivél criar Produto Com Preço Negativo <<<");
        //}
        //var brand = await _brandRepository.Get(input.BrandId);
        //if (brand is null)
        //{
        //    response.Success = false;
        //    response.Message.Add(" >>> Não é possivél criar Produto sem Marca Existente <<<");
        //}
        //if (!response.Success)
        //{
        //    return response;
        //}
        //var update = product;
        //update.Name = input.Name;
        //update.Code = input.Code;
        //update.Stock = input.Stock;
        //update.Price = input.Price;
        //update.Description = input.Description;
        //update.BrandId = input.BrandId;
        //if (update is null)
        //{
        //    response.Message.Add(" >>> Dados Inseridos Incompletos ou Inexistentes <<<");
        //    return response;
        //}
        //await _productRepository.Update(update);
        return new BaseResponse<bool> { Success = true, Message = new List<Notification> { new Notification { Message = " >>> Produto Atualizado com SUCESSO <<<", Type = EnumNotificationType.Success } } };
    }

    public async Task<BaseResponse<bool>> Delete(List<long> idList)
    {
        var response = new BaseResponse<bool>();
        var deleteValidate = await _productValidateService.ValidateDelete(idList);
        if (!deleteValidate.Success)
        {
            response.Success = false;
            response.Message = deleteValidate.Message;
            return response;
        }

        var productList = await _productRepository.Get(idList);
        await _productRepository.Delete(productList);
        foreach (var product in productList)
        {
            response.Message.Add(new Notification { Message = $" >>> Produto: {product.Name} com Id: {product.Id} foi deletado com sucesso <<<", Type = EnumNotificationType.Success });
        }
        return response;
    }
}