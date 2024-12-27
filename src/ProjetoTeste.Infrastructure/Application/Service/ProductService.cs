using ProjetoTeste.Arguments.Arguments.Products;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Interface.Repositories;

namespace ProjetoTeste.Infrastructure.Application.Service;

public class ProductService
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

    public async Task<Response<Product>> ValidationInput(Product input)
    {
        var response = new Response<Product>();
        if (input is null)
        {
            response.Success = false;
            response.Message.Add( ">>> Dados Inseridos Incompletos ou Inexistentes <<<");
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
        return new Response<Product>() { Success = true, Value = input };
    }

    public async Task<Response<List<OutputProduct>>> GetAll()
    {
        var productList = await _productRepository.GetAllAsync();
        return new Response<List<OutputProduct>> { Success = true, Value = productList.ToListOutProducts() };
    }
    public async Task<Response<OutputProduct>> Get(long id)
    {
        var product = await _productRepository.Get(id);
        return new Response<OutputProduct> { Success = true, Value = product.ToOutputProduct() };
    }
    public async Task<Response<OutputProduct>> Delete(long id)
    {
        var response = await ValidationId(id);
        var product = response.Value;
        if (product is null)
        {
            response.Success = false;
            response.Message.Add(" >>> Produto com o Id digitado NÃO encontrado <<<");
        }
        await _productRepository.Delete(id);
        return new Response<OutputProduct> { Success = true, Message = { " >>> Produto deletado com sucesso <<<" }};
    }
    public async Task<Response<OutputProduct>> Create(InputCreateProduct product)
    {
        var productExists = await ValidationInput(product.ToProduct());
        var createProduct = await _productRepository.Create(product.ToProduct());
        if (createProduct is null)
        {
            productExists.Message.Add(" >>> Dados Inseridos Incompletos ou Inexistentes <<<");
            return new Response<OutputProduct> { Success = false, Message = productExists.Message};
        }
        return new Response<OutputProduct> { Success = true, Value = createProduct.ToOutputProduct() };
    }
    public async Task<Response<bool>> Update(long id, InputUpdateProduct product)
    {
        var response = await ValidationInput(product.ToProduct());
        var idExists = await ValidationId(id);
        response.Message.Add(idExists.Message[0]);
        if (!response.Success)
        {
            return new Response<bool>() { Success = false, Message = response.Message };
        }
        var update = idExists.Value;
        update.Name = product.Name;
        update.Code = product.Code;
        update.Stock = product.Stock;
        update.Price = product.Price;
        update.Description = product.Description;
        update.BrandId = product.BrandId;
        if (update is null)
        {
            return new Response<bool> { Success = false, Message = { " >>> Dados Inseridos Incompletos ou Inexistentes <<<" }};
        }
        await _productRepository.Update(update);
        return new Response<bool> { Success = true, Message = { " >>> Produto Atualizado com SUCESSO <<<" }};
    }
}