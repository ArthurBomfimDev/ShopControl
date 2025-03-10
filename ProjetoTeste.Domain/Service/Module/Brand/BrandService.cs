using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository;
using ProjetoTeste.Domain.Interface.Service;
using ProjetoTeste.Domain.Service.Base;
using ProjetoTeste.Infrastructure.Interface.ValidateService;

namespace ProjetoTeste.Domain.Service;

public class BrandService : BaseService<IBrandRepository, IBrandValidateService, BrandDTO, InputCreateBrand, InputUpdateBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand, InputIdentityViewBrand, OutputBrand, BrandValidateDTO>, IBrandService
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

        var listNotification = GetAllNotification();
        if (listNotification.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<List<OutputBrand?>>.Failure(listNotification);

        var listCreateBrand = (from i in RemoveInvalid(listBrandValidate)
                               select new BrandDTO(i.InputCreate.Name, i.InputCreate.Code, i.InputCreate.Description, default)).ToList();

        var listNewBrand = await _brandRepository.Create(listCreateBrand);

        return BaseResult<List<OutputBrand>>.Success(listNewBrand.Select(i => (OutputBrand)i).ToList(), listNotification);
        //response.Content = listNewBrand.Convert<Brand, BrandDTO, OutputBrand>();

    }
    #endregion

    #region Update
    public override async Task<BaseResult<bool>> UpdateMultiple(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        var listOriginalBrand = await _brandRepository.GetListByListId(listInputIdentityUpdateBrand.Select(i => i.Id).ToList());
        var listCodeExists = (await _brandRepository.GetListByListCode(listInputIdentityUpdateBrand.Select(i => i.InputUpdate.Code).ToList())).Select(i => i.Code);
        var listRepeatedInputUpdateIdentify = (from i in listInputIdentityUpdateBrand
                                                    where listInputIdentityUpdateBrand.Count(j => j.Id == i.Id) > 1
                                                    select i.Id).ToList();

        var listRepeatedCode = (from i in listInputIdentityUpdateBrand
                                where listInputIdentityUpdateBrand.Count(j => j.InputUpdate.Code == i.InputUpdate.Code) > 1
                                select i.InputUpdate.Code).ToList();

        var listUpdate = (from i in listInputIdentityUpdateBrand
                          select new
                          {
                              InputIdentityUpdateBrand = i,
                              RepeatedInputUpdate = listRepeatedInputUpdateIdentify.FirstOrDefault(j => j == i.Id),
                              OriginalBrand = listOriginalBrand.FirstOrDefault(k => k.Id == i.Id),
                              RepeatedCode = listRepeatedCode.FirstOrDefault(l => l == i.InputUpdate.Code),
                              CodeExists = listCodeExists.FirstOrDefault(m => m == i.InputUpdate.Code)
                          }).ToList();

        List<BrandValidateDTO> listBrandValidate = listUpdate.Select(i => new BrandValidateDTO().ValidateUpdate(i.InputIdentityUpdateBrand, i.RepeatedInputUpdate, i.OriginalBrand, i.RepeatedCode, i.CodeExists)).ToList();
        _brandValidadeService.ValidateUpdate(listBrandValidate);

        var listNotification = GetAllNotification();

        if (listNotification.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<bool>.Failure(listNotification);

        var listBrandUpdate = (from i in listBrandValidate
                               let name = i.OriginalBrandDTO.Name = i.InputIdentityUpdate.InputUpdate.Name
                               let code = i.OriginalBrandDTO.Code = i.InputIdentityUpdate.InputUpdate.Code
                               let description = i.OriginalBrandDTO.Description = i.InputIdentityUpdate.InputUpdate.Description
                               select i.OriginalBrandDTO).ToList();

        await _brandRepository.Update(listBrandUpdate);

        return BaseResult<bool>.Success(true, listNotification);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> DeleteMultiple(List<InputIdentityDeleteBrand> listInputIdentifyDeleteBrand)
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

        var listNotification = GetAllNotification();

        if (listNotification!.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<bool>.Failure(listNotification!);

        var delete = (from i in listBrandValidate
                      select i.OriginalBrandDTO).ToList();

        await _brandRepository.Delete(delete);

        return BaseResult<bool>.Success(true, listNotification!);
    }
    #endregion
}