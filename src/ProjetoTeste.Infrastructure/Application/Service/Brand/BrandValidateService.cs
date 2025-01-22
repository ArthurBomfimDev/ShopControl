using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class BrandValidateService
{

    #region Create
    public async Task<BaseResponse<List<Brand>>> ValidateCreate(List<BrandValidate> listBrandValidate)
    {
        BaseResponse<List<Brand>> response = new();
        _ = (from i in listBrandValidate
             where i.InputCreate == null
             let SetInvalid = i.SetInvalid()
             select i).ToList();

        _ = (from i in listBrandValidate
             where i.RepeatedInputCreate != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"A marca: '{i.InputCreate.Name}' com o código '{i.InputCreate.Code}' não pode ser cadastrada porque o código está repetido. Por favor, escolha um código diferente.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where i.OriginalBrandDTO != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"A marca: '{i.InputCreate.Name}' com o código '{i.InputCreate.Code}' não pode ser cadastrada porque o código já está em uso. Por favor, escolha um código diferente.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where i.InputCreate.Name.Length > 24 || string.IsNullOrEmpty(i.InputCreate.Name) || string.IsNullOrWhiteSpace(i.InputCreate.Name)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreate.Name.Length > 24 ? $"A marca: '{i.InputCreate.Name}' não pode ser cadastrado porque o nome excede o limite de 24 caracteres."
             : $"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' não pode ser cadastrado porque o nome está vazio.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where i.InputCreate.Code.Length > 6 || string.IsNullOrEmpty(i.InputCreate.Code) || string.IsNullOrWhiteSpace(i.InputCreate.Code)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreate.Code.Length > 6 ? $"A marca: '{i.InputCreate.Name}' possui um código com mais de 6 caracteres e não pode ser cadastrado."
             : $"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' possui um código vazio e não pode ser cadastrado.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where i.InputCreate.Description.Length > 100 || string.IsNullOrEmpty(i.InputCreate.Code) || string.IsNullOrWhiteSpace(i.InputCreate.Code)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreate.Description.Length > 100 ? $"A marca: '{i.InputCreate.Name}' possui uma descrição com mais de 100 caracteres e não pode ser cadastrado."
             : $"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' possui uma descrição vazia e não pode ser cadastrado.")
             select i).ToList();

        var create = (from i in listBrandValidate
                      where !i.Invalid
                      let successMessage = response.AddSuccessMessage($"A marca: '{i.InputCreate.Name}' com o código '{i.InputCreate.Code}' foi cadastrada com sucesso!")
                      select i).Select(i => new Brand(i.InputCreate.Name, i.InputCreate.Code, i.InputCreate.Description, default)).ToList();

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
    public async Task<BaseResponse<List<BrandDTO?>>> ValidateUpdate(List<BrandValidate> listBrandValidate)
    {
        BaseResponse<List<BrandDTO?>> response = new();
        _ = (from i in listBrandValidate
             where i.OriginalBrandDTO == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' com o Id: '{i.InputUpdate.Id}' não pode ser atualizada porque o Id não existe.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where i.RepetedCode != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' com o código '{i.InputUpdate.InputUpdateBrand.Code}' não pode ser atualizada porque o código é repetido. Por favor, escolha um código diferente.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where i.Code != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' com o código '{i.InputUpdate.InputUpdateBrand.Code}' não pode ser atualizada porque o código já está em uso por outra marca. Por favor, escolha um código diferente.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where string.IsNullOrEmpty(i.InputUpdate.InputUpdateBrand.Name) || string.IsNullOrWhiteSpace(i.InputUpdate.InputUpdateBrand.Name) || i.InputUpdate.InputUpdateBrand.Name.Length > 24
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(string.IsNullOrEmpty(i.InputUpdate.InputUpdateBrand.Name) || string.IsNullOrWhiteSpace(i.InputUpdate.InputUpdateBrand.Name) ? $"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' não pode ser cadastrado porque o nome está vazio."
             : $"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' não pode ser cadastrado porque o nome excede o limite de 24 caracteres.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where string.IsNullOrEmpty(i.InputUpdate.InputUpdateBrand.Code) || string.IsNullOrWhiteSpace(i.InputUpdate.InputUpdateBrand.Code) || i.InputUpdate.InputUpdateBrand.Code.Length > 6
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(string.IsNullOrEmpty(i.InputUpdate.InputUpdateBrand.Code) || string.IsNullOrWhiteSpace(i.InputUpdate.InputUpdateBrand.Code) ? $"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' possui um código vazio e não pode ser cadastrado."
             : $"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' possui um código com mais de 6 caracteres e não pode ser cadastrado.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where string.IsNullOrWhiteSpace(i.InputUpdate.InputUpdateBrand.Description) || string.IsNullOrEmpty(i.InputUpdate.InputUpdateBrand.Description) || i.InputUpdate.InputUpdateBrand.Description.Length > 100
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputUpdate.InputUpdateBrand.Description.Length > 100 ? $"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' possui uma descrição com mais de 100 caracteres e não pode ser cadastrado."
             : $"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' possui uma descrição vazia e não pode ser cadastrado.")
             select i).ToList();

        var update = (from i in listBrandValidate
                      where !i.Invalid
                      let successMessage = response.AddSuccessMessage($"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' com o código '{i.InputUpdate.InputUpdateBrand.Code}' foi atualizada com sucesso!")
                      let updateName = i.OriginalBrandDTO.Name = i.InputUpdate.InputUpdateBrand.Name
                      let updateCode = i.OriginalBrandDTO.Code = i.InputUpdate.InputUpdateBrand.Code
                      let updateDescription = i.OriginalBrandDTO.Description = i.InputUpdate.InputUpdateBrand.Description
                      select i.OriginalBrandDTO).ToList();
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
    public async Task<BaseResponse<List<long>>?> ValidateDelete(List<BrandValidate> listBrandValidate)
    {
        var response = new BaseResponse<List<long>>();

        _ = (from i in listBrandValidate
             where i.RepeatedInputCreate != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"O Id: {i.InputIdentifyDeleteBrand.Id} foi digitado repetidas vezes, não é possível deletar a marca com esse Id")
             select i).ToList();

        _ = (from i in listBrandValidate
             where i.OriginalBrandDTO == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"A marca com o Id: {i.InputIdentifyDeleteBrand.Id} não foi encontrada. Por favor, verifique o ID e tente novamente.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where i.BrandWithProduct != 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"A marca com o Id: {i.InputIdentifyDeleteBrand.Id} não pode ser deletada, pois possui produtos cadastrados.")
             select i).ToList();

        var delete = (from i in listBrandValidate
                      where !i.Invalid
                      let message = response.AddSuccessMessage($"A marca com o Id: {i.InputIdentifyDeleteBrand.Id} foi deletada com sucesso.")
                      select i.InputIdentifyDeleteBrand).ToList();
        if (!delete.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = delete.Select(i => i.Id).ToList();
        return response;
    }
    #endregion
}