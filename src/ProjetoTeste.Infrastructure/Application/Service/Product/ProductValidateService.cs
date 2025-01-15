using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;

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

        _ = (from i in listProductValidate
             where i.InputCreateProduct.Name.Length > 24
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O produto '{i.InputCreateProduct.Name}' não pode ser cadastrado porque o nome excede o limite de 24 caracteres.")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputCreateProduct.Code.Length > 16
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O produto '{i.InputCreateProduct.Name}' possui um código com mais de 16 caracteres e não pode ser cadastrado.")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputCreateProduct.Description.Length > 100
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O produto '{i.InputCreateProduct.Name}' possui uma descrição com mais de 100 caracteres e não pode ser cadastrado.")
             select i).ToList();

        var create = (from i in listProductValidate
                      where !i.Invalid
                      let message = response.AddSuccessMessage($"O Produto {i.InputCreateProduct.Name} foi cadastrado com sucesso")
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
             let message = response.AddErrorMessage($"O Produto com Id: {i.InputIdentityUpdateProduct.Id} não pode ser atualizado, por ser repetido")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.Original == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto com Id: {i.InputIdentityUpdateProduct.Id} não pode ser atualizado, por não existir")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.RepeteCode != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto com Id: {i.InputIdentityUpdateProduct.Id} com o código: {i.InputIdentityUpdateProduct.InputUpdateProduct.Code} não pode ser atualizado, por ser repetido")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.OriginalCode != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto com Id: {i.InputIdentityUpdateProduct.Id} com o código: {i.InputIdentityUpdateProduct.InputUpdateProduct.Code} não pode ser atualizado, por já estar em uso")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.BrandId == default
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto com Id: {i.InputIdentityUpdateProduct.Id} com o código de marca: {i.InputIdentityUpdateProduct.InputUpdateProduct.BrandId} não pode ser atualizado, por não existir")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputIdentityUpdateProduct.InputUpdateProduct.Price < 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto com Id: {i.InputIdentityUpdateProduct.Id} não pode ser atualizado, Com preço Negativo")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputIdentityUpdateProduct.InputUpdateProduct.Stock < 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto com Id: {i.InputIdentityUpdateProduct.Id} não pode ser atualizado, Com Stock Negativo")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputIdentityUpdateProduct.InputUpdateProduct.Name.Length > 24
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Produto com Id {i.InputIdentityUpdateProduct.Id} o nome: '{i.InputIdentityUpdateProduct.InputUpdateProduct.Name}' não pode ser cadastrado porque o nome excede o limite de 24 caracteres.")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputIdentityUpdateProduct.InputUpdateProduct.Code.Length > 16
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Produto com Id {i.InputIdentityUpdateProduct.Id} o código: '{i.InputIdentityUpdateProduct.InputUpdateProduct.Name}' possui um código com mais de 16 caracteres e não pode ser cadastrado.")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputIdentityUpdateProduct.InputUpdateProduct.Description.Length > 100
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Produto com Id {i.InputIdentityUpdateProduct.Id} a descrição: '{i.InputIdentityUpdateProduct.InputUpdateProduct.Name}' possui uma descrição com mais de 100 caracteres e não pode ser cadastrado.")
             select i).ToList();

        var update = (from i in listProductValidate
                      where !i.Invalid
                      let message = response.AddSuccessMessage($"O Produto com Id: {i.InputIdentityUpdateProduct.Id} foi atualizado com sucesso")
                      select i).ToList();

        if (!update.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = update;
        return response;
    }
    #endregion

    #region Delete
    public async Task<BaseResponse<List<ProductValidate>>> ValidateDelete(List<ProductValidate> listProductValidate)
    {
        var response = new BaseResponse<List<ProductValidate>>();
        _ = (from i in listProductValidate
             where i.Original == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Produto com Id: {i.InputDeleteProduct} não foi encontrado")
             select i).ToList();

        _ = (from i in listProductValidate
             where !i.Invalid && i.Original.Stock > 0
             let setInvald = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto: {i.Original.Name} com Id: {i.InputDeleteProduct} não pode ser deletado pois possui estoque: {i.Original.Stock}")
             select i).ToList();

        var delete = (from i in listProductValidate
                      where !i.Invalid
                      let message = response.AddSuccessMessage($"O Produto: {i.Original.Name} com Id: {i.InputDeleteProduct} foi deletado com sucesso")
                      select i).ToList();

        if (!delete.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = delete;
        return response;
    }
    #endregion

}