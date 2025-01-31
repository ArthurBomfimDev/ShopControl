using AutoMapper;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Application.Service.Base;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.ValidateService;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class BrandService : BaseService<IBrandRepository, Brand, InputCreateBrand, InputIdentityUpdateBrand, InputIdentifyDeleteBrand, InputIdentityViewBrand, OutputBrand>, IBrandService
{
    #region Dependency Injection
    private readonly IBrandRepository _brandRepository;
    private readonly IBrandValidateService _brandValidadeService;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public BrandService(IBrandRepository brandRepository, IBrandValidateService brandValidadeService, IProductRepository productRepository, IMapper mapper) : base(brandRepository, mapper)
    {
        _brandRepository = brandRepository;
        _brandValidadeService = brandValidadeService;
        _productRepository = productRepository;
        _mapper = mapper;
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
        var listRepeatedInputCreateBrandCode = (from i in listInputCreateBrand
                                                where listInputCreateBrand.Count(j => j.Code == i.Code) > 1
                                                select i.Code).ToList();

        var listCreate = (from i in listInputCreateBrand
                          select new
                          {
                              inputCreateBrand = i,
                              RepeatedInputCreateBrandCode = listRepeatedInputCreateBrandCode.FirstOrDefault(j => j == i.Code),
                              OriginalBrand = (listOriginalBrand.FirstOrDefault(j => j.Code == i.Code)).ToBrandDTO()
                          }).ToList();

        List<BrandValidate> listBrandValidate = listCreate.Select(i => new BrandValidate().ValidateCreate(i.inputCreateBrand, i.RepeatedInputCreateBrandCode, i.OriginalBrand)).ToList();

        var create = await _brandValidadeService.ValidateCreate(listBrandValidate);

        response.Message = create.Message;
        response.Success = create.Success;
        if (!response.Success)
            return response;

        var listCreateBrand = (from i in create.Content
                               let successMessage = response.AddSuccessMessage($"A marca: '{i.InputCreate.Name}' com o código '{i.InputCreate.Code}' foi cadastrada com sucesso!")
                               select new Brand(i.InputCreate.Name, i.InputCreate.Code, i.InputCreate.Description, default)).ToList();

        var listNewBrand = await _brandRepository.Create(listCreateBrand);

        response.Content = listNewBrand.Select(i => i.ToOutputBrand()).ToList();
        return response;
    }
    #endregion

    #region Update
    public async Task<BaseResponse<bool>> Update(InputIdentityUpdateBrand inputidentityupdatebrand)
    {
        return await UpdateMultiple([inputidentityupdatebrand]);
    }

    public async Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        var response = new BaseResponse<bool>();

        var listOriginalBrand = await _brandRepository.GetListByListId(listInputIdentityUpdateBrand.Select(i => i.Id).ToList());
        var listCodeExists = (await _brandRepository.GetListByListCode(listInputIdentityUpdateBrand.Select(i => i.InputUpdateBrand.Code).ToList())).Select(i => i.Code);
        var listRepeatedInputUpdateBrandIdentify = (from i in listInputIdentityUpdateBrand
                                                    where listInputIdentityUpdateBrand.Count(j => j.Id == i.Id) > 1
                                                    select i.Id).ToList();

        var listRepeatedCode = (from i in listInputIdentityUpdateBrand
                                where listInputIdentityUpdateBrand.Count(j => j.InputUpdateBrand.Code == i.InputUpdateBrand.Code) > 1
                                select i.InputUpdateBrand.Code).ToList();

        var listUpdate = (from i in listInputIdentityUpdateBrand
                          select new
                          {
                              InputIdentityUpdateBrand = i,
                              RepeatedInputUpdateBrand = listRepeatedInputUpdateBrandIdentify.FirstOrDefault(j => j == i.Id),
                              OriginalBrand = listOriginalBrand.FirstOrDefault(k => k.Id == i.Id)?.ToBrandDTO(),
                              RepeatedCode = listRepeatedCode.FirstOrDefault(l => l == i.InputUpdateBrand.Code),
                              CodeExists = listCodeExists.FirstOrDefault(m => m == i.InputUpdateBrand.Code)
                          }).ToList();

        List<BrandValidate> listBrandValidate = listUpdate.Select(i => new BrandValidate().ValidateUpdate(i.InputIdentityUpdateBrand, i.RepeatedInputUpdateBrand, i.OriginalBrand, i.RepeatedCode, i.CodeExists)).ToList();

        var update = await _brandValidadeService.ValidateUpdate(listBrandValidate);
        response.Message = update.Message;
        response.Success = update.Success;

        if (!response.Success)
        {
            response.Content = false;
            return response;
        }

        var listBrandUpdate = (from i in update.Content
                               from j in listOriginalBrand
                               where j.Id == i.OriginalBrandDTO?.Id
                               let name = j.Name = i.InputUpdate.InputUpdateBrand.Name
                               let code = j.Code = i.InputUpdate.InputUpdateBrand.Code
                               let description = j.Description = i.InputUpdate.InputUpdateBrand.Description
                               let successMessage = response.AddSuccessMessage($"A marca: '{i.InputUpdate.InputUpdateBrand.Name}' com o código '{i.InputUpdate.InputUpdateBrand.Code}' foi atualizada com sucesso!")
                               select j).ToList();

        response.Content = await _brandRepository.Update(listBrandUpdate);

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