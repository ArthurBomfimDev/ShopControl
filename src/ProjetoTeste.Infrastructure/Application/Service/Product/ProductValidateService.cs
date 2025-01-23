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
             where i.OriginalCode != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto: {i.InputCreateProduct.Name} com o código: {i.InputCreateProduct.Code} não pode ser cadastrado, por já estar em uso")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.BrandId == 0
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
             where i.InputCreateProduct.Name.Length > 24 || string.IsNullOrEmpty(i.InputCreateProduct.Name) || string.IsNullOrWhiteSpace(i.InputCreateProduct.Name)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateProduct.Name.Length > 24 ? $"O produto '{i.InputCreateProduct.Name}' não pode ser cadastrado porque o nome excede o limite de 24 caracteres."
             : $"O produto '{i.InputCreateProduct.Name}' não pode ser cadastrado porque o nome não pode ser vazio.")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputCreateProduct.Code.Length > 6 || string.IsNullOrEmpty(i.InputCreateProduct.Code) || string.IsNullOrWhiteSpace(i.InputCreateProduct.Code)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateProduct.Code.Length > 6 ? $"O produto '{i.InputCreateProduct.Name}' possui um código com mais de 6 caracteres e não pode ser cadastrado."
             : $"O produto '{i.InputCreateProduct.Name}' possui um código vazio e não pode ser cadastrado.")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputCreateProduct.Description.Length > 100 || string.IsNullOrEmpty(i.InputCreateProduct.Description) || string.IsNullOrWhiteSpace(i.InputCreateProduct.Description)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateProduct.Description.Length > 100 ? $"O produto '{i.InputCreateProduct.Name}' possui uma descrição com mais de 100 caracteres e não pode ser cadastrado."
             : $"O produto '{i.InputCreateProduct.Name}' possui uma descrição vazia e não pode ser cadastrado.")
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
             where i.OriginalCode != null && i.Original.Code != i.InputIdentityUpdateProduct.InputUpdateProduct.Code
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
             where i.InputIdentityUpdateProduct.InputUpdateProduct.Name.Length > 24 || string.IsNullOrEmpty(i.InputIdentityUpdateProduct.InputUpdateProduct.Name) || string.IsNullOrEmpty(i.InputIdentityUpdateProduct.InputUpdateProduct.Name)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputIdentityUpdateProduct.InputUpdateProduct.Name.Length > 24 ? $"Produto com Id {i.InputIdentityUpdateProduct.Id} o nome: '{i.InputIdentityUpdateProduct.InputUpdateProduct.Name}' não pode ser cadastrado porque o nome excede o limite de 24 caracteres."
             : $"Produto com Id {i.InputIdentityUpdateProduct.Id} o nome: '{i.InputIdentityUpdateProduct.InputUpdateProduct.Name}' não pode ser cadastrado porque o nome está vazio")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputIdentityUpdateProduct.InputUpdateProduct.Code.Length > 6 || string.IsNullOrEmpty(i.InputIdentityUpdateProduct.InputUpdateProduct.Code) || string.IsNullOrWhiteSpace(i.InputIdentityUpdateProduct.InputUpdateProduct.Code)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputIdentityUpdateProduct.InputUpdateProduct.Code.Length > 6 ? $"Produto com Id {i.InputIdentityUpdateProduct.Id} o código: '{i.InputIdentityUpdateProduct.InputUpdateProduct.Name}' possui um código com mais de 6 caracteres e não pode ser cadastrado."
             : $"Produto com Id {i.InputIdentityUpdateProduct.Id} o código: '{i.InputIdentityUpdateProduct.InputUpdateProduct.Name}' possui um código vazio e não pode ser cadastrado.")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.InputIdentityUpdateProduct.InputUpdateProduct.Description.Length > 100 || string.IsNullOrEmpty(i.InputIdentityUpdateProduct.InputUpdateProduct.Description) || string.IsNullOrWhiteSpace(i.InputIdentityUpdateProduct.InputUpdateProduct.Description)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputIdentityUpdateProduct.InputUpdateProduct.Description.Length > 100 ? $"Produto com Id {i.InputIdentityUpdateProduct.Id} a descrição: '{i.InputIdentityUpdateProduct.InputUpdateProduct.Name}' possui uma descrição com mais de 100 caracteres e não pode ser cadastrado."
             : $"Produto com Id {i.InputIdentityUpdateProduct.Id} a descrição: '{i.InputIdentityUpdateProduct.InputUpdateProduct.Name}' possui uma descrição vazia e não pode ser cadastrado.")
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
             where i.RepetedIdentity != 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Id: {i.RepetedIdentity} foi digitado repetidas vezes, não é possível deletar a marca com esse Id")
             select i).ToList();

        _ = (from i in listProductValidate
             where i.Original == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Produto com Id: {i.InputIdentifyDeleteProduct.Id} não foi encontrado")
             select i).ToList();

        _ = (from i in listProductValidate
             where !i.Invalid && i.Original.Stock > 0
             let setInvald = i.SetInvalid()
             let message = response.AddErrorMessage($"O Produto: {i.Original.Name} com Id: {i.InputIdentifyDeleteProduct} não pode ser deletado pois possui estoque: {i.Original.Stock}")
             select i).ToList();

        var delete = (from i in listProductValidate
                      where !i.Invalid
                      let message = response.AddSuccessMessage($"O Produto: {i.Original.Name} com Id: {i.InputIdentifyDeleteProduct.Id} foi deletado com sucesso")
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