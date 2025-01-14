using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entity;

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
    public async Task<BaseResponse<OutputBrand>> Create(InputCreateBrand inputCreateBrand)
    {
        var response = new BaseResponse<OutputBrand>();
        var result = await CreateMultiple([inputCreateBrand]);
        response.Message = result.Message;
        response.Success = result.Success;
        response.Content = result.Content.FirstOrDefault();
        return response;
    }

    public async Task<BaseResponse<List<OutputBrand>>> CreateMultiple(List<InputCreateBrand> listInputCreateBrand)
    {
        var response = new BaseResponse<List<OutputBrand>>();
        var listOriginalBrand = await _brandRepository.GetListByListCode(listInputCreateBrand.Select(i => i.Code).ToList());
        var _listRepeatedInputCreateBrand = (from i in listInputCreateBrand
                                             where listInputCreateBrand.Count(j => j.Code == i.Code) > 1
                                             select i).ToList();
        var listCreate = (from i in listInputCreateBrand
                          select new
                          {
                              inputCreateBrand = i,
                              RepeatedInputCreateBrand = _listRepeatedInputCreateBrand.FirstOrDefault(j => j.Code == i.Code),
                              OriginalBrand = (listOriginalBrand.FirstOrDefault(j => j.Code == i.Code)).ToBrandDTO()
                          }).ToList();
        List<BrandValidate> listBrandValidate = listCreate.Select(i => new BrandValidate().ValidateCreate(i.inputCreateBrand, i.RepeatedInputCreateBrand, i.OriginalBrand)).ToList();
        var create = await _brandValidadeService.ValidateCreate(listBrandValidate);
        await _brandRepository.Create(create.Content);
        response.Message = create.Message;
        response.Success = create.Success;
        response.Content = create.Content.Select(i => i.ToOutputBrand()).ToList();
        return response;
    }
    #endregion

    #region Update
    public async Task<BaseResponse<bool>> Update(InputIdentityUpdateBrand inputidentityupdatebrand)
    {
        var result = await UpdateMultiple([inputidentityupdatebrand]);
        return result;
    }

    public async Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        var response = new BaseResponse<bool>();
        List<BrandDTO> listOriginalBrandDTO = (await _brandRepository.GetListByListId(listInputIdentityUpdateBrand.Select(i => i.Id).ToList())).Select(i => i.ToBrandDTO()).ToList();
        List<BrandDTO> listCode = (await _brandRepository.GetListByListCode(listInputIdentityUpdateBrand.Select(i => i.InputUpdateBrand.Code).ToList())).Select(i => i.ToBrandDTO()).ToList();
        var _listRepeatedInputUpdateBrand = (from i in listInputIdentityUpdateBrand
                                             where listInputIdentityUpdateBrand.Count(j => j.Id == i.Id) > 1
                                             select i).ToList();

        var _listRepeatedCode = (from i in listInputIdentityUpdateBrand
                                             where listInputIdentityUpdateBrand.Count(j => j.InputUpdateBrand.Code == i.InputUpdateBrand.Code) > 1
                                             select i).ToList();

        var listUpdate = (from i in listInputIdentityUpdateBrand
                          select new
                          {
                              InputIdentityUpdateBrand = i,
                              RepeatedInputUpdateBrand = _listRepeatedInputUpdateBrand.FirstOrDefault(j => j.Id == i.Id),
                              OriginalBrand = listOriginalBrandDTO.FirstOrDefault(j => j.Id == i.Id),
                              RepeatedCode = _listRepeatedCode.FirstOrDefault(j => j.InputUpdateBrand.Code == i.InputUpdateBrand.Code),
                              Code = listCode.FirstOrDefault(j => j.Code == i.InputUpdateBrand.Code && j.Id != i.Id)
                          }).ToList();

        List<BrandValidate> listBrandValidate = listUpdate.Select(i => new BrandValidate().ValidateUpdate(i.InputIdentityUpdateBrand, i.RepeatedInputUpdateBrand, i.OriginalBrand, i.RepeatedCode, i.Code)).ToList();

        var update = await _brandValidadeService.ValidateUpdate(listBrandValidate);
        response.Message = update.Message;
        response.Success = update.Success;

        if (!response.Success)
        {
            response.Content = false;
            return response;
        }

        var listUpdateBrand = update.Content;
        var olderBrand = await _brandRepository.GetListByListId(listUpdateBrand.Select(i => i.Id).ToList());

        //Fazer um jeito melhor
        for(int i = 0;  i < olderBrand.Count; i++)
        {
            olderBrand[i].Name = listUpdateBrand[i].Name;
            olderBrand[i].Code = listUpdateBrand[i].Code;
            olderBrand[i].Description = listUpdateBrand[i].Description;
        }

        response.Content = await _brandRepository.Update(olderBrand);

        return response;
    }
    #endregion

    #region Delete
    public Task<BaseResponse<bool>> Delete(long id)
    {
        var vali
    }

    public Task<BaseResponse<bool>> DeleteMultiple(List<long> listId)
    {
        var validatecreate = await _brandvalidadeservice.validadedelete(listid);
        var response = new baseresponse<bool>() { message = validatecreate.message };

        if (!validatecreate.success)
        {
            response.success = false;
            return response;
        }

        var branddelete = await _brandrepository.getlistbylistid(validatecreate.content);
        await _brandrepository.delete(branddelete);

        foreach (var id in validatecreate.content)
        {
            response.addsuccessmessage($" >>> marca com id: {id} deletada com sucesso <<<");
        }
        return response;
    }
    #endregion
    

    
    {
        throw new NotImplementedException();
    }

    public async Task<List<Brand>> GetDuplicates(List<Brand> listBrand, Brand item)
    {
        return listBrand.Where(other => !ReferenceEquals(item, other) && other.Code == item.Code).ToList();
    }
}