using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ProjetoTeste.Infrastructure.Application;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly BrandValidateService _brandValidadeService;

    public BrandService(IBrandRepository brandRepository, BrandValidateService brandValidadeService)
    {
        _brandRepository = brandRepository;
        _brandValidadeService = brandValidadeService;
    }

    #region Get
    public async Task<List<OutputBrand>> GetAll()
    {
        var brandList = await _brandRepository.GetAll();
        return (from i in brandList select i.ToOutputBrand()).ToList();
    }

    public async Task<OutputBrand> Get(long id)
    {
        var brand = await _brandRepository.Get(id);
        return brand.ToOutputBrand();
    }

    public async Task<List<OutputBrand>> GetListByListId(List<long> listId)
    {
        var brand = await _brandRepository.GetListByListId(listId);
        return (from i in brand
                select i.ToOutputBrand()).ToList();
    }
    #endregion

    #region Create
    async Task<BaseResponse<OutputBrand>> Create(InputCreateBrand inputCreateBrand)
    {
        var result = await _brandRepository.CreateMultiple([inputCreateBrand]);
        return result?.FirtOrDefault;
    }

    public async Task<BaseResponse<List<OutputBrand>>> CreateMultiple(List<InputCreateBrand> listInputCreateBrand)
    {
        var response = new BaseResponse<List<OutputBrand>>();
        var listCode = listInputCreateBrand.Select(i => i.Code).ToList();
        var listOriginalBrand = await _brandRepository.GetListByListCode(listInputCreateBrand.Select(i => i.Code).ToList());
        var _listRepeatedInputCreateBrand = (from i in listInputCreateBrand
                                             where listInputCreateBrand.Count(j => j.Code == i.Code) > 1
                                             select i).ToList();
        var listCreate = (from i in listInputCreateBrand
                          select new
                          {
                              inputCreateBrand = i,
                              RepeatedInputCreateBrand = _listRepeatedInputCreateBrand.FirstOrDefault(j => j.Code == i.Code),
                              OriginalBrand = listOriginalBrand.FirstOrDefault(j => j.Code == i.Code)
                          }).ToList();

        var listBrandValidate = await BrandValidate(listCreate);

        foreach (var brand in listCreate)
        {
            if(brand.OriginalBrand != null)
            {
                response.AddErrorMessage($" O Produto: {brand.inputCreateBrand.Name} com o código: {brand.inputCreateBrand.Code} não pode ser cadastrado por já estar em uso");
                brand.inputCreateBrand = null;
            }
            if (brand.listRepeatedInputCreateBrand)
            {
                response.AddErrorMessage($" O Produto: {brand.inputCreateBrand.Name} com o código: {brand.inputCreateBrand.Code} não pode ser cadastrado por ser repetido");
            }
            new Brand(brand.inputCreateBrand.Name, brand.inputCreateBrand.Code, brand.inputCreateBrand.Description, default);
        }

        //var validateCreate = await _brandValidadeService.ValidateCreate(listInputCreateBrand);
        //var response = new BaseResponse<List<OutputBrand>>() { Message = validateCreate.Message };
        //if (!validateCreate.Success)
        //{
        //    response.Success = false;
        //    return response;
        //}

        //var brand = (from i in validateCreate.Content
        //             select new Brand(i.Name, i.Code, i.Description, default)).ToList();

        //var createBrand = await _brandRepository.Create(brand);

        //if (createBrand.Count() == 0)
        //{
        //    response.AddSuccessMessage(" >>> ERRO - Marca não criada - Dados digitados errados ou incompletos <<<");
        //    return response;
        //}

        //response.Content = (from i in createBrand select i.ToOutputBrand()).ToList();
        //return response;
    }
    #endregion

    #region Update
    Task<BaseResponse<bool>> Update(InputIdentityUpdateBrand inputIdentityUpdateBrand)
    {
        var result = await _brandRepository.UpdateMultiple([inputCreateBrand]);
        return result?.FirtOrDefault
    }

    public async Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        var validateUpdate = await _brandValidadeService.ValidateUpdate(listInputIdentityUpdateBrand);
        var response = new BaseResponse<bool>() { Message = validateUpdate.Message };

        if (!validateUpdate.Success)
        {
            response.Success = false;
            return response;
        }
        var brandUpdate = await _brandRepository.Update(validateUpdate.Content);

        if (!brandUpdate)
        {
            response.Success = false;
            response.AddErrorMessage(" >>> Não foi possivel atualizar a Marca <<<");
            return response;
        }

        foreach (var item in validateUpdate.Content)
        {
            response.AddSuccessMessage($" >>> A Marca: {item.Name} com Id: {item.Id} foi Atualizada com SUCESSO <<<");
        }
        return response;
    }
    #endregion

    #region Delete
    Task<BaseResponse<bool>> Delete(long id)
    {
        var result = await _brandRepository.DeleteMultiple([inputCreateBrand]);
        return result?.FirtOrDefault
        }

    public async Task<BaseResponse<bool>> DeleteMultiple(List<long> listId)
    {
        var validateCreate = await _brandValidadeService.ValidadeDelete(listId);
        var response = new BaseResponse<bool>() { Message = validateCreate.Message };

        if (!validateCreate.Success)
        {
            response.Success = false;
            return response;
        }

        var brandDelete = await _brandRepository.GetListByListId(validateCreate.Content);
        await _brandRepository.Delete(brandDelete);

        foreach (var id in validateCreate.Content)
        {
            response.AddSuccessMessage($" >>> Marca com Id: {id} deletada com sucesso <<<");
        }
        return response;
    }
    #endregion

    public static async Task<List<Brand>> GetDuplicates(this List<Brand> listBrand, Brand item)
    {
        return listBrand.Where(other => !ReferenceEquals(item, other) && other.Code == item.Code).ToList();
    }

}