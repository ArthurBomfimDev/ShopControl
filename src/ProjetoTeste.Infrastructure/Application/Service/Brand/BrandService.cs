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
    #region Dependency Injection
    private readonly IBrandRepository _brandRepository;
    private readonly BrandValidateService _brandValidadeService;
    private readonly IProductRepository _productRepository;

    public BrandService(IBrandRepository brandRepository, BrandValidateService brandValidadeService, IProductRepository productRepository)
    {
        _brandRepository = brandRepository;
        _brandValidadeService = brandValidadeService;
        _productRepository = productRepository;
    }
    #endregion

    #region Get
    public async Task<List<OutputBrand>> GetAll()
    {
        var brandList = await _brandRepository.GetAll();
        return (from i in brandList select i.ToOutputBrand()).ToList();
    }

    public async Task<OutputBrand> Get(InputIdentifyViewBrand inputIdentifyViewBrand)
    {
        var brand = await _brandRepository.Get(inputIdentifyViewBrand.Id);
        return brand.ToOutputBrand();
    }

    public async Task<List<OutputBrand>> GetListByListId(List<InputIdentifyViewBrand> listInputIdentifyViewBrand)
    {
        var brand = await _brandRepository.GetListByListId(listInputIdentifyViewBrand.Select(i => i.Id).ToList());
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
        if (!response.Success)
            return response;
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
        response.Message = create.Message;
        response.Success = create.Success;
        if (!response.Success)
            return response;

        var listNewBrand = await _brandRepository.Create(create.Content.Select(i => new Brand(i.InputCreate.Name, i.InputCreate.Code, i.InputCreate.Description, default)).ToList());
        response.Content = listNewBrand.Select(i => i.ToOutputBrand()).ToList();
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
        var listOriginalBrandDTO = await _brandRepository.GetListByListId(listInputIdentityUpdateBrand.Select(i => i.Id).ToList());
        var listCode = await _brandRepository.GetListByListCode(listInputIdentityUpdateBrand.Select(i => i.InputUpdateBrand.Code).ToList());
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
                              OriginalBrand = listOriginalBrandDTO.FirstOrDefault(k => k.Id == i.Id).ToBrandDTO(),
                              RepeatedCode = _listRepeatedCode.FirstOrDefault(l => l.InputUpdateBrand.Code == i.InputUpdateBrand.Code),
                              Code = listCode.FirstOrDefault(m => m.Code == i.InputUpdateBrand.Code && m.Id != i.Id).ToBrandDTO()
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
        var listOldBrand = await _brandRepository.GetListByListId(listUpdateBrand.Select(i => i.Id).ToList());

        //Fazer um jeito melhor
        for (int i = 0; i < listOldBrand.Count; i++)
        {
            listOldBrand[i].Name = listUpdateBrand[i].Name;
            listOldBrand[i].Code = listUpdateBrand[i].Code;
            listOldBrand[i].Description = listUpdateBrand[i].Description;
        }

        response.Content = await _brandRepository.Update(listOldBrand);

        return response;
    }
    #endregion

    #region Delete
    public Task<BaseResponse<bool>> Delete(InputIdentifyDeleteBrand inputIdentifyDeleteBrand)
    {
        return DeleteMultiple([inputIdentifyDeleteBrand]);
    }

    public async Task<BaseResponse<bool>> DeleteMultiple(List<InputIdentifyDeleteBrand> listInputIdentifyDeleteBrand)
    {
        var response = new BaseResponse<bool>();

        var listOriginalBrand = await _brandRepository.GetListByListId(listInputIdentifyDeleteBrand.Select(i => i.Id).ToList());

        var listRepeteInputDelete = (from i in listInputIdentifyDeleteBrand
                                     where listInputIdentifyDeleteBrand.Count(j => j.Id == i.Id) > 1
                                     select i).ToList();

        var listBrandWithProduct = await _productRepository.BrandId(listInputIdentifyDeleteBrand.Select(i => i.Id).ToList());

        var listDelete = (from i in listInputIdentifyDeleteBrand
                          select new
                          {
                              InputDeleteBrand = i,
                              OriginalBrand = listOriginalBrand.FirstOrDefault(j => j.Id == i.Id).ToBrandDTO(),
                              RepeteInputDelete = listRepeteInputDelete.FirstOrDefault(k => k == i),
                              BrandWithProduct = listBrandWithProduct.FirstOrDefault(l => l == i.Id)
                          }).ToList();
        List<BrandValidate> listBrandValidate = listDelete.Select(i => new BrandValidate().ValidateDelete(i.InputDeleteBrand, i.OriginalBrand, i.RepeteInputDelete, i.BrandWithProduct)).ToList();

        var deleteValidate = await _brandValidadeService.ValidateDelete(listBrandValidate);
        response.Message = deleteValidate.Message;
        response.Success = deleteValidate.Success;
        if (!response.Success)
        {
            response.Content = false;
            return response;
        }

        var listBrandDelete = await _brandRepository.GetListByListId(deleteValidate.Content);
        response.Content = await _brandRepository.Delete(listBrandDelete);
        return response;
    }
    #endregion
}