using ProjetoEstagioAPI.Arguments.Products;
using ProjetoEstagioAPI.Infrastructure.UnitOfWork;
using ProjetoEstagioAPI.Mapping.Products;
using ProjetoEstagioAPI.Models;

namespace ProjetoEstagioAPI.Services;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<Product>> ValidationId(long id)
    {
        var productExist = await _unitOfWork.ProductRepository.Get(id);
        if (productExist is null)
        {
            return new Response<Product>() { Success = false, Message = " >>> Produto com o Id digitado NÃO encontrado <<<" };
        }
        return new Response<Product>() { Success = true, Value = productExist };
    }

    public async Task<Response<Product>> ValidationInput(Product input)
    {
        if (input is null)
        {
            return new Response<Product> { Success = false, Message = " >>> Dados Inseridos Incompletos ou Inexistentes <<<" };

        }
        var productList = await _unitOfWork.ProductRepository.GetAllAsync();
        bool nameExists = productList.Any(p => p.Name == input.Name);
        if (nameExists)
        {
            return new Response<Product> { Success = false, Message = " >>> Já existe um Produto Cadastrado com esse Nome <<<" };
        }
        bool codeExists = productList.Any(p => p.Code == input.Code);
        if (codeExists)
        {
            return new Response<Product> { Success = false, Message = " >>> Já existe um Produto Cadastrado com esse Código <<<" };
        }
        if (input.Stock < 0)
        {
            return new Response<Product> { Success = false, Message = " >>> Não é possivél criar Produto Com Stock Negativo <<<" };
        }
        if (input.Price < 0)
        {
            return new Response<Product> { Success = false, Message = " >>> Não é possivél criar Produto Com Preço Negativo <<<" };
        }
        var brand = await _unitOfWork.BrandRepository.Get(input.BrandId);
        if (brand is null)
        {
            return new Response<Product> { Success = false, Message = " >>> Não é possivél criar Produto sem Marca Existente <<<" };
        }
        return new Response<Product>() { Success = true, Value = input };
    }
        
    public async Task<Response<List<OutputProduct>>> GetAll()
    {
        var productList = await _unitOfWork.ProductRepository.GetAllAsync();
        if (productList is null || productList.Count == 0)
        {
            return new Response<List<OutputProduct>> { Message = " >>> Não há produtos cadastrados no sistema <<<", Success = false };
        }
        return new Response<List<OutputProduct>> { Success = true, Value = productList.ToListOutProducts() };
    }
    public async Task<Response<OutputProduct>> Get(long id)
    {
        var product = await _unitOfWork.ProductRepository.Get(id);
        if (product is null)
        {
            return new Response<OutputProduct> { Success = false, Message = " >>> Produto com o Id digitado NÃO encontrado <<<" };
        }
        return new Response<OutputProduct> { Success = true, Value = product.ToOutputProduct() };
    }
    public async Task<Response<OutputProduct>> Delete(long id)
    {
        var product = await _unitOfWork.ProductRepository.Get(id);
        if (product is null)
        {
            return new Response<OutputProduct> { Success = false, Message = " >>> Produto com o Id digitado NÃO encontrado <<<" };
        }
        await _unitOfWork.ProductRepository.Delete(id);
        return new Response<OutputProduct> { Success = true, Message = " >>> Produto deletado com sucesso <<<" };
    }
    public async Task<Response<OutputProduct>> Create(InputCreateProduct product)
    {
        var productExists = await ValidationInput(product.ToProduct());
        if (!productExists.Success)
        {
            return new Response<OutputProduct>() { Success = false, Message = productExists.Message };
        }
        var createProduct = await _unitOfWork.ProductRepository.Create(product.ToProduct());
        if (createProduct is null)
        {
            return new Response<OutputProduct> { Success = false, Message = " >>> Dados Inseridos Incompletos ou Inexistentes <<<" };
        }
        await _unitOfWork.Commit();
        return new Response<OutputProduct> { Success = true, Value = createProduct.ToOutputProduct() };
    }
    public async Task<Response<OutputProduct>> Update(long id, InputUpdateProduct product)
    {
        var idExists = await ValidationId(id);
        if (idExists.Success)
        {
            return new Response<OutputProduct>() { Success = false, Message = idExists.Message };
        }
        var productExists = await ValidationInput(product.ToProduct());
        if (!productExists.Success)
        {
            return new Response<OutputProduct>() { Success = false, Message = productExists.Message };
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
            return new Response<OutputProduct> { Success = false, Message = " >>> Dados Inseridos Incompletos ou Inexistentes <<<" };
        }
        await _unitOfWork.ProductRepository.Update(update);
        await _unitOfWork.Commit();
        return new Response<OutputProduct> { Success = true, Message = " >>> Produto Atualizado com SUCESSO <<<" };

    }
}