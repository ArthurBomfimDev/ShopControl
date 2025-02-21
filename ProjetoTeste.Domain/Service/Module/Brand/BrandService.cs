using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository;
using ProjetoTeste.Infrastructure.Application.Service.Base;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.ValidateService;

namespace ProjetoTeste.Domain.Service;

public class BrandService : BaseService<IBrandRepository, IBrandValidateService, BrandDTO, InputCreateBrand, InputIdentityUpdateBrand, InputIdentifyDeleteBrand, InputIdentityViewBrand, OutputBrand, BrandValidateDTO>, IBrandService
{
    #region Dependency Injection
    private readonly IBrandRepository _brandRepository;
    private readonly IBrandValidateService _brandValidadeService;
    private readonly IProductRepository _productRepository;

    public BrandService(IBrandRepository brandRepository, IBrandValidateService brandValidadeService, IProductRepository productRepository) : base(brandRepository, brandValidadeService)
    {
        _brandRepository = brandRepository;
        _brandValidadeService = brandValidadeService;
        _productRepository = productRepository;
    }
    #endregion

    #region Create
    public override async Task<BaseResult<List<OutputBrand?>>> CreateMultiple(List<InputCreateBrand> listInputCreateBrand)
    {
        var listOriginalBrand = await _brandRepository.GetListByListCode(listInputCreateBrand.Select(i => i.Code).ToList());
        var listRepeatedInputCreateBrandCode = (from i in listInputCreateBrand
                                                where listInputCreateBrand.Count(j => j.Code == i.Code) > 1
                                                select i.Code).ToList();

        var listCreate = (from i in listInputCreateBrand
                          select new
                          {
                              inputCreateBrand = i,
                              RepeatedInputCreateBrandCode = listRepeatedInputCreateBrandCode.FirstOrDefault(j => j == i.Code),
                              OriginalBrand = listOriginalBrand.FirstOrDefault(j => j.Code == i.Code)
                          }).ToList();

        List<BrandValidateDTO> listBrandValidate = listCreate.Select(i => new BrandValidateDTO().ValidateCreate(i.inputCreateBrand, i.RepeatedInputCreateBrandCode, i.OriginalBrand)).ToList();
        _brandValidadeService.ValidateCreate(listBrandValidate);

        var (success, errors) = GetValidationResult();
        if (errors.Count == listInputCreateBrand.Count)
            return BaseResult<List<OutputBrand?>>.Failure(errors);

        var listCreateBrand = (from i in RemoveInvalid(listBrandValidate)
                               select new BrandDTO(i.InputCreate.Name, i.InputCreate.Code, i.InputCreate.Description, default)).ToList();

        var listNewBrand = await _brandRepository.Create(listCreateBrand);

        return BaseResult<List<OutputBrand>>.Success(listNewBrand.Select(i => (OutputBrand)(BrandDTO)i).ToList(), [.. success, .. errors]);
        //response.Content = listNewBrand.Convert<Brand, BrandDTO, OutputBrand>();

    }
    #endregion

    #region Update
    public override async Task<BaseResult<bool>> UpdateMultiple(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
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
                              OriginalBrand = listOriginalBrand.FirstOrDefault(k => k.Id == i.Id),
                              RepeatedCode = listRepeatedCode.FirstOrDefault(l => l == i.InputUpdateBrand.Code),
                              CodeExists = listCodeExists.FirstOrDefault(m => m == i.InputUpdateBrand.Code)
                          }).ToList();

        List<BrandValidateDTO> listBrandValidate = listUpdate.Select(i => new BrandValidateDTO().ValidateUpdate(i.InputIdentityUpdateBrand, i.RepeatedInputUpdateBrand, i.OriginalBrand, i.RepeatedCode, i.CodeExists)).ToList();
        _brandValidadeService.ValidateUpdate(listBrandValidate);

        var (success, errors) = GetValidationResult();

        if (success.Count == 0)
            return BaseResult<bool>.Failure(errors);

        var listBrandUpdate = (from i in listBrandValidate
                               let name = i.OriginalBrandDTO.Name = i.InputUpdate.InputUpdateBrand.Name
                               let code = i.OriginalBrandDTO.Code = i.InputUpdate.InputUpdateBrand.Code
                               let description = i.OriginalBrandDTO.Description = i.InputUpdate.InputUpdateBrand.Description
                               select i.OriginalBrandDTO).ToList();

        await _brandRepository.Update(listBrandUpdate);

        return BaseResult<bool>.Success(true, [.. success, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> DeleteMultiple(List<InputIdentifyDeleteBrand> listInputIdentifyDeleteBrand)
    {
        var listOriginalBrand = await _brandRepository.GetListByListId(listInputIdentifyDeleteBrand.Select(i => i.Id).ToList());

        var listRepeteInputDelete = (from i in listInputIdentifyDeleteBrand
                                     where listInputIdentifyDeleteBrand.Count(j => j.Id == i.Id) > 1
                                     select i).ToList();

        var listBrandWithProduct = await _productRepository.BrandId(listInputIdentifyDeleteBrand.Select(i => i.Id).ToList());

        var listDelete = (from i in listInputIdentifyDeleteBrand
                          select new
                          {
                              InputDeleteBrand = i,
                              OriginalBrand = listOriginalBrand.FirstOrDefault(j => j.Id == i.Id),
                              RepeteInputDelete = listRepeteInputDelete.FirstOrDefault(k => k == i),
                              BrandWithProduct = listBrandWithProduct.FirstOrDefault(l => l == i.Id)
                          }).ToList();

        List<BrandValidateDTO> listBrandValidate = listDelete.Select(i => new BrandValidateDTO().ValidateDelete(i.InputDeleteBrand, i.OriginalBrand, i.RepeteInputDelete, i.BrandWithProduct)).ToList();
        _brandValidadeService.ValidateDelete(listBrandValidate);

        var (success, errors) = GetValidationResult();

        if (success.Count == 0)
            return BaseResult<bool>.Failure(errors);

        var delete = (from i in listBrandValidate
                      select i.OriginalBrandDTO).ToList();

        await _brandRepository.Delete(delete);

        return BaseResult<bool>.Success(true, [.. success, .. errors]);
    }
    #endregion
}