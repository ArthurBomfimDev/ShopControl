using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System.Runtime.ConstrainedExecution;

namespace ProjetoTeste.Infrastructure.Application;

public class ProductValidateService
{
    #region Create
    public async Task<BaseResponse<List<ProductValidate>>> ValidateCreate(List<ProductValidate> listProductValidate)
    {
        var response = new BaseResponse<List<ProductValidate>>();
        _ = (from i in listProductValidate
             where i.RepeteCode != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto: {i.InputCreateProduct.Name} com o código: {i.InputCreateProduct.Code} não pode ser cadastrado, por ser repetido")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.Original != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto: {i.InputCreateProduct.Name} com o código: {i.InputCreateProduct.Code} não pode ser cadastrado, por já estar em uso")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.BrandId == default
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto: {i.InputCreateProduct.Name} com o código de marca: {i.InputCreateProduct.BrandId} não pode ser cadastrado, por não existir")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputCreateProduct.Price < 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto: {i.InputCreateProduct.Name} não pode ser cadastrado, Com preço Negativo")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputCreateProduct.Stock < 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto: {i.InputCreateProduct.Name} não pode ser cadastrado, Com Stock Negativo")
             select i).ToList();

        var create = (from i in listProductValidate
                      where !i.Invalid
                      select i).ToList();

        if (!create.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = create;
        return response;
    }
    #endregion

    #region Update
    public async Task<BaseResponse<List<ProductValidate>>> ValidateUpdate(List<ProductValidate> listProductValidate)
    {
        var response = new BaseResponse<List<ProductValidate>>();
        _ = (from i in listProductValidate
             where i.RepeteIdentity == default
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.Original == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.RepeteCode == default
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.OriginalCode == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.
        //if (idList.Count != inputUpdateList.Count())
        //{
        //    response.Success = false;
        //    response.AddErrorMessage(" >>> ERRO - A Quantidade de Id's Digitados é Diferente da Quantidade de Produtos <<<");
        //    return response;
        //}
        //var validateIdList = idList;
        //var validateInputList = inputUpdateList;

        //var notIdExist = (from i in idList
        //                  where _productRepository.ProductExists(i) == false
        //                  select idList.IndexOf(i)).ToList();
        //if (notIdExist.Any())
        //{
        //    for (int i = 0; i < notIdExist.Count; i++)
        //    {
        //        response.AddErrorMessage($" >>> Marca com id: {idList[notIdExist[i]]} não encontrada <<<");
        //        validateIdList.RemoveAt(notIdExist[i]);
        //        validateInputList.RemoveAt(notIdExist[i]);
        //    }
        //}
        //if (!idList.Any())
        //{
        //    response.Success = false;
        //    return response;
        //}

        //var repeatedCode = (from i in inputUpdateList
        //                    where inputUpdateList.Count(j => j.Code == i.Code) > 1
        //                    select inputUpdateList.IndexOf(i)).ToList();
        //if (repeatedCode.Any())
        //{
        //    for (int i = 0; i < repeatedCode.Count; i++)
        //    {
        //        response.AddErrorMessage($" >>> O Produto: {inputUpdateList[repeatedCode[i]].Name} com o código: {inputUpdateList[repeatedCode[i]].Code} não pode ser cadastrado, por ser repetido <<<");
        //        idList.Remove(idList[repeatedCode[i]]);
        //        inputUpdateList.Remove(inputUpdateList[repeatedCode[i]]);
        //    }
        //}
        //if (!idList.Any())
        //{
        //    response.Success = false;
        //    return response;
        //}

        //var validateStock = (from i in inputUpdateList
        //                     where i.Stock < 0
        //                     select inputUpdateList.IndexOf(i)).ToList();
        //if (validateStock.Any())
        //{
        //    for (int i = 0; i < validateStock.Count; i++)
        //    {
        //        response.AddErrorMessage($" >>> O Produto: {inputUpdateList[validateStock[i]].Name} não pode ser atualizado com estoque negativo <<<");
        //        idList.Remove(idList[validateStock[i]]);
        //        inputUpdateList.Remove(inputUpdateList[validateStock[i]]);
        //    }
        //}
        //if (!idList.Any())
        //{
        //    response.Success = false;
        //    return response;
        //}

        //var validatePrice = (from i in inputUpdateList
        //                     where i.Price < 0
        //                     select inputUpdateList.IndexOf(i)).ToList();
        //if (validatePrice.Any())
        //{
        //    for (int i = 0; i < validateStock.Count; i++)
        //    {
        //        response.AddErrorMessage($" >>> O Produto: {inputUpdateList[validatePrice[i]].Name} não pode ser atualizado com preço negativo <<<");
        //        idList.Remove(idList[validatePrice[i]]);
        //        inputUpdateList.Remove(inputUpdateList[validatePrice[i]]);
        //    }
        //}
        //if (!idList.Any())
        //{
        //    response.Success = false;
        //    return response;
        //}

        //var brandExists = (from i in inputUpdateList
        //                       //where _brandRepository.BrandExists(i.BrandId) == false
        //                   select inputUpdateList.IndexOf(i)).ToList();
        //if (brandExists.Any())
        //{
        //    for (int i = 0; i < brandExists.Count; i++)
        //    {
        //        response.AddErrorMessage($" >>> O Produto: {inputUpdateList[brandExists[i]].Name} não pode ser atualizado, pois a Marca não existe <<<");
        //        idList.Remove(idList[brandExists[i]]);
        //        inputUpdateList.Remove(inputUpdateList[brandExists[i]]);
        //    }
        //}
        //if (!idList.Any())
        //{
        //    response.Success = false;
        //    return response;
        //}

        //var productList = await _productRepository.GetListByListId(idList);
        //var validateCode = (from i in inputUpdateList
        //                    where i.Code != productList[inputUpdateList.IndexOf(i)].Code && _productRepository.CodeExists(i.Code) == true
        //                    select inputUpdateList.IndexOf(i)).ToList();
        //if (validateCode.Any())
        //{
        //    for (int i = 0; i < validateCode.Count; i++)
        //    {
        //        response.AddErrorMessage($" >>> O Produto: {inputUpdateList[validateCode[i]].Name} com o códgio: {inputUpdateList[validateCode[i]].Code} não pode ser atualizado, por já estar em Uso <<<");
        //        inputUpdateList.Remove(inputUpdateList[validateCode[i]]);
        //        productList.Remove(productList[validateCode[i]]);
        //    }
        //}
        //if (!idList.Any())
        //{
        //    response.Success = false;
        //    return response;
        //}

        //for (int i = 0; i < inputUpdateList.Count(); i++)
        //{
        //    productList[i].Name = inputUpdateList[i].Name;
        //    productList[i].Code = inputUpdateList[i].Code;
        //    productList[i].Description = inputUpdateList[i].Description;
        //    productList[i].Price = inputUpdateList[i].Price;
        //    productList[i].BrandId = inputUpdateList[i].BrandId;
        //    productList[i].Stock = inputUpdateList[i].Stock;
        //}

        //response.Content = productList;
        return response;
    }
    #endregion
    public async Task<BaseResponse<List<long>>> ValidateDelete(List<long> idList)
    {
        var response = new BaseResponse<List<long>>();
        //var idNotExists = (from i in idList
        //                   where _productRepository.ProductExists(i) == false
        //                   select i).ToList();

        //if (idNotExists.Any())
        //{
        //    foreach (var id in idNotExists)
        //    {
        //        response.AddErrorMessage($" >>> O Produto com Id: {id} não existe <<<");
        //    }
        //    idList = idList.Except(idNotExists).ToList();
        //}

        //if (!idList.Any())
        //{
        //    response.Success = false;
        //    return response;
        //}

        //response.Content = idList;
        return response;
    }

}