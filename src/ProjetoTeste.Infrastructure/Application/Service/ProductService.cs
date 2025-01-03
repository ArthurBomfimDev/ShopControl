using ProjetoTeste.Arguments.Arguments.Products;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;

    public ProductService(IProductRepository productRepository, IBrandRepository brandRepository)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
    }

    public async Task<Response<Product>> ValidationId(long id)
    {
        var productExist = await _productRepository.Get(id);
        if (productExist is null)
        {
            return new Response<Product>() { Success = false, Message = { " >>> Produto com o Id digitado NÃO encontrado <<<" } };
        }
        return new Response<Product>() { Success = true, Value = productExist };
    }

    public async Task<Response<List<OutputProduct>>> GetAll()
    {
        var productList = await _productRepository.GetAllAsync();
        return new Response<List<OutputProduct>> { Success = true, Value = (from i in productList select i.ToOutputProduct()).ToList() };
    }

    public async Task<Response<OutputProduct>> Get(long id)
    {
        var product = await _productRepository.Get(id);
        return new Response<OutputProduct> { Success = true, Value = product.ToOutputProduct() };
    }

    public async Task<Response<Product>> ValidationInput(Product input)
    {
        var response = new Response<Product>();
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
        return new Response<Product>() { Success = true, Value = input };
    }

    public async Task<Response<OutputProduct>> Create(InputCreateProduct product)
    {
        var productExists = await ValidationInput(product.ToProduct());
        if (!productExists.Success)
        {
            return new Response<OutputProduct> { Success = false, Message = productExists.Message };
        }
        var createProduct = await _productRepository.Create(product.ToProduct());
        return new Response<OutputProduct> { Success = true, Value = createProduct.ToOutputProduct() };
    }

    public async Task<Response<bool>> Update(long id, InputUpdateProduct input)
    {
        var response = new Response<bool>();
        var idExists = await ValidationId(id);
        var product = idExists.Value;
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
        return new Response<bool> { Success = true, Message = { " >>> Produto Atualizado com SUCESSO <<<" } };
    }

    public async Task<Response<bool>> Delete(long id)
    {
        var response = await ValidationId(id);
        var product = response.Value;
        if (product is null)
        {
            response.Success = false;
            response.Message.Add(" >>> Produto com o Id digitado NÃO encontrado <<<");
        }
        await _productRepository.Delete(id);
        return new Response<bool> { Success = true, Message = { " >>> Produto deletado com sucesso <<<" } };
    }

}