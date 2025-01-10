using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Interface.Repositories;

namespace ProjetoTeste.Infrastructure.Application.Service.Product;

public class ProductValidateService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;

    public ProductValidateService(IProductRepository productRepository, IBrandRepository brandRepository)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponse<List<InputCreateProduct>>> ValidateCreate(List<InputCreateProduct> inputCreateList)
    {
        var response = new BaseResponse<List<InputCreateProduct>>();

        var repetedCode = (from i in inputCreateList
                           where inputCreateList.Count(j => j.Code == i.Code) > 1
                           select i).ToList();

       // var repetedCode = inputCreateList
    .GroupBy(p => p.Code)
    .Where(g => g.Count() > 1)
    .SelectMany(g => g)
    .ToList();
        jeito melhor usar o groupby

        if (repetedCode.Any())
        {
            foreach (var product in repetedCode)
            {
                response.Message.Add(new Notification { Message = $" >>> O Produto: {product.Name} com o código: {product.Code} não pode ser cadastrado, por ser repetido <<<", Type = EnumNotificationType.Error });
            }
            inputCreateList = inputCreateList.Except(repetedCode).ToList();
        }
        if (!inputCreateList.Any())
        {
            response.Success = false;
            return response;
        }

        var codeExists = (from i in inputCreateList
                          where _productRepository.CodeExists(i.Code) == true
                          select i).ToList();
        if (codeExists.Any())
        {
            foreach (var product in codeExists)
            {
                response.Message.Add(new Notification { Message = $" >>> O Produto: {product.Name} com o código: {product.Code} não pode ser cadastrado, por já estar em uso <<<", Type = EnumNotificationType.Error });
            }
            inputCreateList = inputCreateList.Except(codeExists).ToList();
        }
        if (!inputCreateList.Any())
        {
            response.Success = false;
            return response;
        }

        var brandExists = (from i in inputCreateList
                           where _brandRepository.BrandExists(i.BrandId) == false
                           select i).ToList();
        if (brandExists.Any())
        {
            foreach (var product in brandExists)
            {
                response.Message.Add(new Notification { Message = $" >>> O Produto: {product.Name} com o código de marca: {product.BrandId} não pode ser cadastrado, por não existir <<<", Type = EnumNotificationType.Error });
            }
            inputCreateList = inputCreateList.Except(brandExists).ToList();
        }
        if (!inputCreateList.Any())
        {
            response.Success = false;
            return response;
        }

        var validateStock = (from i in inputCreateList
                             where i.Stock < 0
                             select i).ToList();
        if (validateStock.Any())
        {
            foreach (var product in validateStock)
            {
                response.Message.Add(new Notification { Message = $" >>> O Produto: {product.Name} não pode ser cadastrado, Com Stock Negativo <<<", Type = EnumNotificationType.Error });
            }
            inputCreateList = inputCreateList.Except(validateStock).ToList();
        }
        if (!inputCreateList.Any())
        {
            response.Success = false;
            return response;
        }

        var validatePrice = (from i in inputCreateList
                             where i.Price < 0
                             select i).ToList();
        if (validatePrice.Any())
        {
            foreach (var product in validatePrice)
            {
                response.Message.Add(new Notification { Message = $" >>> O Produto: {product.Name} não pode ser cadastrado, Com preço Negativo <<<", Type = EnumNotificationType.Error });
            }
            inputCreateList = inputCreateList.Except(validatePrice).ToList();
        }
        if (!inputCreateList.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = inputCreateList;
        return response;
    }
    
    public async Task<BaseResponse<List<long>>> ValidateDelete(List<long> idList)
    {
        var response = new BaseResponse<List<long>>();
        var brandExists = (from i in idList
                           where _productRepository.ProductExists(i) == true
                           select i).ToList();
        if(brandExists.Any())
        {
            foreach (var id in brandExists)
            {
                response.Message.Add(new Notification { Message = $" >>> O Produto com Id: {id} não existe <<<", Type = EnumNotificationType.Error });
            }
            idList = idList.Except(brandExists).ToList();
        }
        if(brandExists.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content= idList;
        return response;
    }

}