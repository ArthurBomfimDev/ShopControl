using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class ProductValidateService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;

    public ProductValidateService(IProductRepository productRepository, IBrandRepository brandRepository)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponse<List<InputCreateProduct>>> ValidateCreate(List<InputCreateProduct> listInputCreateProduct)
    {
        var response = new BaseResponse<List<InputCreateProduct>>();

        var repetedCode = (from i in listInputCreateProduct
                           where listInputCreateProduct.Count(j => j.Code == i.Code) > 1
                           select i).ToList();

        // var repetedCode = ListinputCreate
        //.GroupBy(p => p.Code)
        //.Where(g => g.Count() > 1)
        //.SelectMany(g => g)
        //.ToList();
        //    jeito melhor usar o groupby

        if (repetedCode.Any())
        {
            foreach (var product in repetedCode)
            {
                response.AddErrorMessage($" >>> O Produto: {product.Name} com o código: {product.Code} não pode ser cadastrado, por ser repetido <<<");
            }
            listInputCreateProduct = listInputCreateProduct.Except(repetedCode).ToList(); // set invalid
        }
        if (!listInputCreateProduct.Any())
        {
            response.Success = false;
            return response;
        }

        var codeExists = (from i in listInputCreateProduct
                          where _productRepository.CodeExists(i.Code) == true
                          select i).ToList();
        if (codeExists.Any())
        {
            foreach (var product in codeExists)
            {
                response.AddErrorMessage($" >>> O Produto: {product.Name} com o código: {product.Code} não pode ser cadastrado, por já estar em uso <<<");
            }
            listInputCreateProduct = listInputCreateProduct.Except(codeExists).ToList();
        }
        if (!listInputCreateProduct.Any())
        {
            response.Success = false;
            return response;
        }

        var brandExists = (from i in listInputCreateProduct
                           where _brandRepository.BrandExists(i.BrandId) == false
                           select i).ToList();
        if (brandExists.Any())
        {
            foreach (var product in brandExists)
            {
                response.AddErrorMessage($" >>> O Produto: {product.Name} com o código de marca: {product.BrandId} não pode ser cadastrado, por não existir <<<");
            }
            listInputCreateProduct = listInputCreateProduct.Except(brandExists).ToList();
        }
        if (!listInputCreateProduct.Any())
        {
            response.Success = false;
            return response;
        }

        var validateStock = (from i in listInputCreateProduct
                             where i.Stock < 0
                             select i).ToList();
        if (validateStock.Any())
        {
            foreach (var product in validateStock)
            {
                response.AddErrorMessage($" >>> O Produto: {product.Name} não pode ser cadastrado, Com Stock Negativo <<<");
            }
            listInputCreateProduct = listInputCreateProduct.Except(validateStock).ToList();
        }
        if (!listInputCreateProduct.Any())
        {
            response.Success = false;
            return response;
        }

        var validatePrice = (from i in listInputCreateProduct
                             where i.Price < 0
                             select i).ToList();
        if (validatePrice.Any())
        {
            foreach (var product in validatePrice)
            {
                response.AddErrorMessage($" >>> O Produto: {product.Name} não pode ser cadastrado, Com preço Negativo <<<");
            }
            listInputCreateProduct = listInputCreateProduct.Except(validatePrice).ToList();
        }
        if (!listInputCreateProduct.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = listInputCreateProduct;
        return response;
    }

    public async Task<BaseResponse<List<Product>>> ValidateUpdate(List<long> idList, List<InputUpdateProduct> inputUpdateList)
    {
        var response = new BaseResponse<List<Product>>();
        if (idList.Count != inputUpdateList.Count())
        {
            response.Success = false;
            response.AddErrorMessage(" >>> ERRO - A Quantidade de Id's Digitados é Diferente da Quantidade de Produtos <<<");
            return response;
        }
        var validateIdList = idList;
        var validateInputList = inputUpdateList;

        var notIdExist = (from i in idList
                          where _productRepository.ProductExists(i) == false
                          select idList.IndexOf(i)).ToList();
        if (notIdExist.Any())
        {
            for (int i = 0; i < notIdExist.Count; i++)
            {
                response.AddErrorMessage($" >>> Marca com id: {idList[notIdExist[i]]} não encontrada <<<");
                validateIdList.RemoveAt(notIdExist[i]);
                validateInputList.RemoveAt(notIdExist[i]);
            }
        }
        if (!idList.Any())
        {
            response.Success = false;
            return response;
        }

        var repeatedCode = (from i in inputUpdateList
                            where inputUpdateList.Count(j => j.Code == i.Code) > 1
                            select inputUpdateList.IndexOf(i)).ToList();
        if (repeatedCode.Any())
        {
            for (int i = 0; i < repeatedCode.Count; i++)
            {
                response.AddErrorMessage($" >>> O Produto: {inputUpdateList[repeatedCode[i]].Name} com o código: {inputUpdateList[repeatedCode[i]].Code} não pode ser cadastrado, por ser repetido <<<");
                idList.Remove(idList[repeatedCode[i]]);
                inputUpdateList.Remove(inputUpdateList[repeatedCode[i]]);
            }
        }
        if (!idList.Any())
        {
            response.Success = false;
            return response;
        }

        var validateStock = (from i in inputUpdateList
                             where i.Stock < 0
                             select inputUpdateList.IndexOf(i)).ToList();
        if (validateStock.Any())
        {
            for (int i = 0; i < validateStock.Count; i++)
            {
                response.AddErrorMessage($" >>> O Produto: {inputUpdateList[validateStock[i]].Name} não pode ser atualizado com estoque negativo <<<");
                idList.Remove(idList[validateStock[i]]);
                inputUpdateList.Remove(inputUpdateList[validateStock[i]]);
            }
        }
        if (!idList.Any())
        {
            response.Success = false;
            return response;
        }

        var validatePrice = (from i in inputUpdateList
                             where i.Price < 0
                             select inputUpdateList.IndexOf(i)).ToList();
        if (validatePrice.Any())
        {
            for (int i = 0; i < validateStock.Count; i++)
            {
                response.AddErrorMessage($" >>> O Produto: {inputUpdateList[validatePrice[i]].Name} não pode ser atualizado com preço negativo <<<");
                idList.Remove(idList[validatePrice[i]]);
                inputUpdateList.Remove(inputUpdateList[validatePrice[i]]);
            }
        }
        if (!idList.Any())
        {
            response.Success = false;
            return response;
        }

        var brandExists = (from i in inputUpdateList
                           where _brandRepository.BrandExists(i.BrandId) == false
                           select inputUpdateList.IndexOf(i)).ToList();
        if (brandExists.Any())
        {
            for (int i = 0; i < brandExists.Count; i++)
            {
                response.AddErrorMessage($" >>> O Produto: {inputUpdateList[brandExists[i]].Name} não pode ser atualizado, pois a Marca não existe <<<");
                idList.Remove(idList[brandExists[i]]);
                inputUpdateList.Remove(inputUpdateList[brandExists[i]]);
            }
        }
        if (!idList.Any())
        {
            response.Success = false;
            return response;
        }

        var productList = await _productRepository.GetListByListId(idList);
        var validateCode = (from i in inputUpdateList
                            where i.Code != productList[inputUpdateList.IndexOf(i)].Code && _productRepository.CodeExists(i.Code) == true
                            select inputUpdateList.IndexOf(i)).ToList();
        if (validateCode.Any())
        {
            for (int i = 0; i < validateCode.Count; i++)
            {
                response.AddErrorMessage($" >>> O Produto: {inputUpdateList[validateCode[i]].Name} com o códgio: {inputUpdateList[validateCode[i]].Code} não pode ser atualizado, por já estar em Uso <<<");
                inputUpdateList.Remove(inputUpdateList[validateCode[i]]);
                productList.Remove(productList[validateCode[i]]);
            }
        }
        if (!idList.Any())
        {
            response.Success = false;
            return response;
        }

        for (int i = 0; i < inputUpdateList.Count(); i++)
        {
            productList[i].Name = inputUpdateList[i].Name;
            productList[i].Code = inputUpdateList[i].Code;
            productList[i].Description = inputUpdateList[i].Description;
            productList[i].Price = inputUpdateList[i].Price;
            productList[i].BrandId = inputUpdateList[i].BrandId;
            productList[i].Stock = inputUpdateList[i].Stock;
        }

        response.Content = productList;
        return response;
    }

    public async Task<BaseResponse<List<long>>> ValidateDelete(List<long> idList)
    {
        var response = new BaseResponse<List<long>>();
        var idNotExists = (from i in idList
                           where _productRepository.ProductExists(i) == false
                           select i).ToList();

        if (idNotExists.Any())
        {
            foreach (var id in idNotExists)
            {
                response.AddErrorMessage($" >>> O Produto com Id: {id} não existe <<<");
            }
            idList = idList.Except(idNotExists).ToList();
        }

        if (!idList.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = idList;
        return response;
    }

}