using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application.Service;
namespace ProjetoTeste.Infrastructure.Application.Service.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;

    public ProductService(IProductRepository productRepository, IBrandRepository brandRepository)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponse<Product>> ValidationId(long id)
    {
        var productExist = await _productRepository.Get(id);
        if (productExist is null)
        {
            return new BaseResponse<Product>() { Success = false, Message = { " >>> Produto com o Id digitado NÃO encontrado <<<" } };
        }
        return new BaseResponse<Product>() { Success = true, Content = productExist };
    }

    public async Task<BaseResponse<List<OutputProduct>>> GetAll()
    {
        var productList = await _productRepository.GetAllAsync();
        return new BaseResponse<List<OutputProduct>> { Success = true, Content = (from i in productList select i.ToOutputProduct()).ToList() };
    }

    public async Task<BaseResponse<OutputProduct>> Get(long id)
    {
        var product = await _productRepository.Get(id);
        return new BaseResponse<OutputProduct> { Success = true, Content = product.ToOutputProduct() };
    }

    public async Task<BaseResponse<Product>> ValidationInput(Product input)
    {
        var response = new BaseResponse<Product>();
        if (input is null)
        {
            response.Success = false;
            response.Message.Add(">>> Dados Inseridos Incompletos ou Inexistentes <<<");
        }
        bool codeExists = await _productRepository.Exist(input.Code);
        if (codeExists)
        {
            response.Success = false;
            response.Message.Add(" >>> Já existe um Produto Cadastrado com esse Código <<<");
        }
        if (input.Stock < 0)
        {
            response.Success = false;
            response.Message.Add(" >>> Não é possivél criar Produto Com Stock Negativo <<<");
        }
        if (input.Price < 0)
        {
            response.Success = false;
            response.Message.Add(" >>> Não é possivél criar Produto Com Preço Negativo <<<");
        }
        var brand = await _brandRepository.Get(input.BrandId);
        if (brand is null)
        {
            response.Success = false;
            response.Message.Add(" >>> Não é possivél criar Produto sem Marca Existente <<<");
        }
        if (!response.Success)
        {
            return response;
        }
        return new BaseResponse<Product>() { Success = true, Content = input };
    }

    public async Task<BaseResponse<OutputProduct>> Create(InputCreateProduct product)
    {
        var productExists = await ValidationInput(product.ToProduct());
        if (!productExists.Success)
        {
            return new BaseResponse<OutputProduct> { Success = false, Message = productExists.Message };
        }
        var createProduct = await _productRepository.Create(product.ToProduct());
        return new BaseResponse<OutputProduct> { Success = true, Content = createProduct.ToOutputProduct() };
    }

    public async Task<BaseResponse<bool>> Update(long id, InputUpdateProduct input)
    {
        var response = new BaseResponse<bool>();
        var idExists = await ValidationId(id);
        var product = idExists.Content;
        if (!idExists.Success)
        {
            response.Message.Add(idExists.Message[0]);
        }
        if (input is null)
        {
            response.Success = false;
            response.Message.Add(">>> Dados Inseridos Incompletos ou Inexistentes <<<");
        }
        bool codeExists = await _productRepository.Exist(input.Code);
        if (input.Code != product.Code && codeExists)
        {
            response.Success = false;
            response.Message.Add(" >>> Já existe um Produto Cadastrado com esse Código <<<");
        }
        if (input.Stock < 0)
        {
            response.Success = false;
            response.Message.Add(" >>> Não é possivél criar Produto Com Stock Negativo <<<");
        }
        if (input.Price < 0)
        {
            response.Success = false;
            response.Message.Add(" >>> Não é possivél criar Produto Com Preço Negativo <<<");
        }
        var brand = await _brandRepository.Get(input.BrandId);
        if (brand is null)
        {
            response.Success = false;
            response.Message.Add(" >>> Não é possivél criar Produto sem Marca Existente <<<");
        }
        if (!response.Success)
        {
            return response;
        }
        var update = product;
        update.Name = input.Name;
        update.Code = input.Code;
        update.Stock = input.Stock;
        update.Price = input.Price;
        update.Description = input.Description;
        update.BrandId = input.BrandId;
        if (update is null)
        {
            response.Message.Add(" >>> Dados Inseridos Incompletos ou Inexistentes <<<");
            return response;
        }
        await _productRepository.Update(update);
        return new BaseResponse<bool> { Success = true, Message = { " >>> Produto Atualizado com SUCESSO <<<" } };
    }

    public async Task<BaseResponse<bool>> Delete(long id)
    {
        var response = await ValidationId(id);
        var product = response.Content;
        if (product is null)
        {
            response.Success = false;
            response.Message.Add(" >>> Produto com o Id digitado NÃO encontrado <<<");
        }
        await _productRepository.Delete(id);
        return new BaseResponse<bool> { Success = true, Message = { " >>> Produto deletado com sucesso <<<" } };
    }

}