using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System.Linq;

namespace ProjetoTeste.Infrastructure.Application;

public class BrandValidateService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IProductRepository _productRepository;

    public BrandValidateService(IBrandRepository brandRepository, IProductRepository productRepository)
    {
        _brandRepository = brandRepository;
        _productRepository = productRepository;
    }

    #region Create
    public async Task<BaseResponse<List<Brand>>> ValidateCreate(List<BrandValidate> listBrandValidate)
    {
        BaseResponse<List<Brand>> response = new();
        _ = (from i in listBrandValidate
             where i.RepeatedInputCreate != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"A marca: '{i.InputCreate.Name}' com o código '{i.InputCreate.Code}' não pode ser cadastrada porque o código já está em uso. Por favor, escolha um código diferente.")
             select i).ToList();

        _ = (from i in listBrandValidate
             where i.OriginalBrandDTO != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"A marca: '{i.InputCreate.Name}' com o código '{i.InputCreate.Code}' não pode ser cadastrada porque o código já está em uso. Por favor, escolha um código diferente.")
             select i).ToList();

        var create = (from i in listBrandValidate
                      where !i.Invalid
                      let successMessage = response.AddSuccessMessage($"A marca: '{i.InputCreate.Name}' com o código '{i.InputCreate.Code}' foi cadastrada com sucesso!")
                      select i).Select(i => new Brand(i.InputCreate.Name, i.InputCreate.Code, i.InputCreate.Description, default)).ToList();

        response.Content = create;
        return response;
    }
    #endregion

    #region Update
    public async Task<BaseResponse<List<BrandDTO?>>> ValidateUpdate(List<BrandValidate> listBrandValidate)
    {
        BaseResponse<List<BrandDTO?>> response = new();
        _ = (from i in listBrandValidate
             where i.RepetedInputUpdate != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' com o Id: '{i.InputUpdate.Id}' não pode ser atualizada porque o Id éstá repetido.")
             select i).ToList();

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

        var update = (from i in listBrandValidate
                      where !i.Invalid
                      let successMessage = response.AddSuccessMessage($"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' com o código '{i.InputUpdate.InputUpdateBrand.Code}' foi atualizada com sucesso!")
                      let updateName = i.OriginalBrandDTO.Name = i.InputUpdate.InputUpdateBrand.Name
                      let updateCode = i.OriginalBrandDTO.Code = i.InputUpdate.InputUpdateBrand.Code
                      let updateDescription = i.OriginalBrandDTO.Description = i.InputUpdate.InputUpdateBrand.Description
                      select i.OriginalBrandDTO).ToList();
        if(!update.Any())
        {
            response.Success = false;
            return response;
        }
        response.Content = update;
        return response;
    }
    #endregion
    //public async Task<BaseResponse<List<InputCreateBrand>>> ValidateCreate(List<InputCreateBrand> listInputCreateBrand)
    //{
    //    var response = new BaseResponse<List<InputCreateBrand>>();
    //    if (!listInputCreateBrand.Any())
    //    {
    //        response.Success = false;
    //        response.AddErrorMessage(" >>> Dados Inseridos Inválidos <<<");
    //        return response;
    //    }

    //    validar tamanhos por exemplo code, nome description...

    //    var repeatedCode = (from i in input
    //                        where input.Count(j => j.Code == i.Code) > 1
    //                        select i).ToList();

    //    var repeatedCode = (from i in (from j in listInputCreateBrand
    //                                   group j by j.Code into g
    //                                   where g.Count() > 1
    //                                   select g).ToList()
    //                        from k in i
    //                        select k).ToList();

    //    foreach (var brand in repeatedCode)
    //    {
    //        response.AddErrorMessage($" >>> Erro - A marca: de nome: {brand.Name} o código: {brand.Code} não pode ser cadastrado, por ser repetido <<<");
    //        listInputCreateBrand.Remove(brand);
    //    }

    //    if (!listInputCreateBrand.Any())
    //    {
    //        response.Success = false;
    //        return response;
    //    }

    //    var codeExists = (from i in listInputCreateBrand
    //                      where _brandRepository.CodeExists(i.Code) == true
    //                      select i).ToList();
    //    foreach (var item in codeExists)
    //    {
    //        response.AddErrorMessage($" >>> Erro - A marca: de nome: {item.Name} o código: {item.Code} não pode ser cadastrado, por já estar em uso <<<");
    //    }

    //    if (codeExists.Count == listInputCreateBrand.Count)
    //    {
    //        response.Success = false;
    //        return response;
    //    }

    //    var validateCreate = (listInputCreateBrand.Except(codeExists)).ToList();
    //    response.Content = validateCreate;
    //    return response;
    //}

    //public async Task<BaseResponse<List<Brand>>> ValidateUpdate(List<long> ids, List<InputUpdateBrand> input)
    //{
    //    var response = new BaseResponse<List<Brand>>();
    //    if (ids.Count() != input.Count)
    //    {
    //        response.Success = false;
    //        response.AddErrorMessage(" >>> ERRO - A Quantidade de Id's Digitados é Diferente da Quantidade de Marcas <<<");
    //        return response;
    //    }

    //    var notIdExist = (from i in ids
    //                      where _brandRepository.BrandExists(i) == false
    //                      select ids.IndexOf(i)).ToList();
    //    if (notIdExist.Any())
    //    {
    //        for (int i = 0; i < notIdExist.Count; i++)
    //        {
    //            response.AddErrorMessage($" >>> Marca com id: {ids[notIdExist[i]]} não encontrada <<<");
    //            ids.Remove(ids[notIdExist[i]]);
    //            input.Remove(input[notIdExist[i]]);
    //        }
    //    }
    //    if (!input.Any())
    //    {
    //        response.Success = false;
    //        return response;
    //    }

    //    var repeatedCode = (from i in input
    //                        where input.Count(j => j.Code == i.Code) > 1
    //                        select input.IndexOf(i)).ToList();
    //    if (repeatedCode.Any())
    //    {
    //        for (int i = 0; i < repeatedCode.Count; i++)
    //        {
    //            response.AddErrorMessage($" >>> Erro - A marca: de nome: {input[repeatedCode[i]].Name} o código: {input[repeatedCode[i]].Code} não pode ser cadastrado, por ser repetido <<<");
    //            ids.Remove(ids[repeatedCode[i]]);
    //            input.Remove(input[repeatedCode[i]]);
    //        }
    //    }
    //    if (!input.Any())
    //    {
    //        response.Success = false;
    //        return response;
    //    }

    //    var brandList = await _brandRepository.GetListByListId(ids);

    //    var codeExists = (from i in input
    //                      let index = input.IndexOf(i)
    //                      where _brandRepository.CodeExists(i.Code) == true && i.Code != brandList[index].Code
    //                      select input.IndexOf(i)).ToList();

    //    if (codeExists.Any())
    //    {
    //        for (int i = 0; i < codeExists.Count; i++)
    //        {
    //            response.AddErrorMessage($" >>> Marca: {input[codeExists[i]].Name} o código: {input[codeExists[i]].Code} não pode ser alterado - Em uso por outra marca: <<<");
    //            ids.Remove(ids[codeExists[i]]);
    //            input.Remove(input[codeExists[i]]);
    //            brandList.Remove(brandList[codeExists[i]]);
    //        }
    //    }
    //    if (!input.Any())
    //    {
    //        response.Success = false;
    //        return response;
    //    }

    //    for (int i = 0; i < brandList.Count(); i++)
    //    {
    //        brandList[i].Name = input[i].Name;
    //        brandList[i].Code = input[i].Code;
    //        brandList[i].Description = input[i].Description;
    //    }

    //    response.Content = brandList;
    //    return response;
    //}

    //public async Task<BaseResponse<List<long>>> ValidadeDelete(List<long> ids)
    //{
    //    var response = new BaseResponse<List<long>>();
    //    var idExist = (from i in ids
    //                   where _brandRepository.BrandExists(i) == false
    //                   select i).ToList();

    //    if (idExist.Any())
    //    {
    //        foreach (var id in idExist)
    //        {
    //            response.AddErrorMessage($" >>> Marca com Id: {id} não Existe <<<");
    //        }
    //        ids = (ids.Except(idExist)).ToList();
    //    }

    //    var withProduct = await _productRepository.BrandId(ids);
    //    if (withProduct.Any())
    //    {
    //        foreach (var id in withProduct)
    //        {
    //            response.AddErrorMessage($" >>> Marca com id: {id} não pode ser deletada - Possui produtos <<<");
    //        }
    //    }

    //    List<long> withoutProduct = (ids.Except(withProduct)).ToList();
    //    if (!withoutProduct.Any())
    //    {
    //        response.Success = false;
    //        return response;
    //    }

    //    response.Content = withoutProduct;
    //    return response;
    //}
}