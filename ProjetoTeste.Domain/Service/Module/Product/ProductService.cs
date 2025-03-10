using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository;
using ProjetoTeste.Domain.Interface.Service;
using ProjetoTeste.Domain.Service.Base;
using ProjetoTeste.Infrastructure.Interface.ValidateService;

namespace ProjetoTeste.Domain.Service;
public class ProductService : BaseService<IProductRepository, IProductValidateService, ProductDTO, InputCreateProduct, InputUpdateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentityViewProduct, OutputProduct, ProductValidateDTO>, IProductService
{
    #region Dependency Injection
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IProductValidateService _productValidateService;

    public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, IProductValidateService productValidateService) : base(productRepository, productValidateService)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _productValidateService = productValidateService;
    }
    #endregion

    #region Get
    public async Task<List<OutputProduct>> GetListByBrandId(InputIdentityViewBrand inputIdentifyViewBrand)
    {
        var listByBrandId = await _productRepository.GetListByBrandId(inputIdentifyViewBrand.Id);
        return listByBrandId.Select(i => (OutputProduct)i).ToList();
    }
    #endregion

    #region Create
    public override async Task<BaseResult<List<OutputProduct>>> CreateMultiple(List<InputCreateProduct> listinputCreateProduct)
    {
        var response = new BaseResponse<List<OutputProduct>>();

        var listOriginalCode = (await _productRepository.GetListByCodeList(listinputCreateProduct.Select(i => i.Code).ToList())).Select(j => j.Code);
        var listRepeteCode = (from i in listinputCreateProduct
                              where listinputCreateProduct.Count(j => j.Code == i.Code) > 1
                              select i.Code).ToList();
        var listBrand = (await _brandRepository.GetListByListId(listinputCreateProduct.Select(i => i.BrandId).ToList())).Select(i => i.Id);

        var listCreate = (from i in listinputCreateProduct
                          select new
                          {
                              InputCreateProduct = i,
                              OriginalCode = listOriginalCode.FirstOrDefault(j => j == i.Code),
                              RepeteCode = listRepeteCode.FirstOrDefault(k => k == i.Code),
                              BrandExists = listBrand.FirstOrDefault(l => l == i.BrandId)
                          }).ToList();

        List<ProductValidateDTO> listProductValidate = listCreate.Select(i => new ProductValidateDTO().ValidateCreate(i.InputCreateProduct, i.OriginalCode, i.RepeteCode, i.BrandExists)).ToList();
        _productValidateService.ValidateCreate(listProductValidate);

        var listNotification = GetAllNotification();

        if (listNotification!.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<List<OutputProduct>>.Failure(listNotification!);

        var listCreateProduct = (from i in listProductValidate
                                 select i.InputCreate).Select(i => new ProductDTO(i.Name, i.Code, i.Description, i.Price, i.BrandId, i.Stock, default)).ToList();

        var listCreatedProduct = await _productRepository.Create(listCreateProduct);

        return BaseResult<List<OutputProduct>>.Success(listCreatedProduct!.Select(i => (OutputProduct)i).ToList(), listNotification!);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<bool>> UpdateMultiple(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct)
    {
        var listOriginalIdentity = await _productRepository.GetListByListId(listInputIdentityUpdateProduct.Select(i => i.Id).ToList());
        var listRepeteIdentity = (from i in listInputIdentityUpdateProduct
                                  where listInputIdentityUpdateProduct.Count(j => j.Id == i.Id) > 1
                                  select i.Id).ToList();
        var listOriginalCode = (await _productRepository.GetListByCodeList(listInputIdentityUpdateProduct.Select(i => i.InputUpdate.Code).ToList())).Select(i => i.Code);
        var listRepeteCode = (from i in listInputIdentityUpdateProduct
                              where listInputIdentityUpdateProduct.Count(j => j.InputUpdate.Code == i.InputUpdate.Code) > 1
                              select i.InputUpdate.Code).ToList();
        var listBrand = (await _brandRepository.GetListByListId(listInputIdentityUpdateProduct.Select(i => i.InputUpdate.BrandId).ToList())).Select(i => i.Id);

        var listUpdate = (from i in listInputIdentityUpdateProduct
                          select new
                          {
                              InputIdentityUpdateProduct = i,
                              OriginalIdentity = listOriginalIdentity.FirstOrDefault(j => j.Id == i.Id),
                              RepeteIdentity = listRepeteIdentity.FirstOrDefault(k => k == i.Id),
                              OriginalCode = listOriginalCode.FirstOrDefault(l => l == i.InputUpdate.Code),
                              RepeteCode = listRepeteCode.FirstOrDefault(m => m == i.InputUpdate.Code),
                              BrandExists = listBrand.FirstOrDefault(n => n == i.InputUpdate.BrandId)
                          }).ToList();

        List<ProductValidateDTO> listProductValidate = listUpdate.Select(i => new ProductValidateDTO().ValidateUpdate(i.InputIdentityUpdateProduct, i.OriginalIdentity, i.OriginalCode, i.RepeteIdentity, i.RepeteCode, i.BrandExists)).ToList();
        _productValidateService.ValidateUpdate(listProductValidate);

        var listNotification = GetAllNotification();

        if (listNotification!.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<bool>.Failure(listNotification!);

        var listUpdateProduct = (from i in listProductValidate
                                 let name = i.Original.Name = i.InputIdentityUpdate.InputUpdate.Name
                                 let code = i.Original.Code = i.InputIdentityUpdate.InputUpdate.Code
                                 let description = i.Original.Description = i.InputIdentityUpdate.InputUpdate.Description
                                 let brandId = i.Original.BrandId = i.InputIdentityUpdate.InputUpdate.BrandId
                                 let stock = i.Original.Stock = i.InputIdentityUpdate.InputUpdate.Stock
                                 let Price = i.Original.Price = i.InputIdentityUpdate.InputUpdate.Price
                                 select i.Original).ToList();

        await _productRepository.Update(listUpdateProduct);
        return BaseResult<bool>.Success(true, listNotification!);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> DeleteMultiple(List<InputIdentityDeleteProduct> listInputIdentifyDeleteProduct)
    {
        var response = new BaseResponse<bool>();

        var listRepetedIdentity = (from i in listInputIdentifyDeleteProduct
                                   where listInputIdentifyDeleteProduct.Count(j => j.Id == i.Id) > 1
                                   select i.Id).ToList();
        var listOriginalIdentity = await _productRepository.GetListByListId(listInputIdentifyDeleteProduct.Select(i => i.Id).ToList());
        var listProductDelete = (from i in listInputIdentifyDeleteProduct
                                 select new
                                 {
                                     InputDeleteProduct = i,
                                     listOriginalIdentity = listOriginalIdentity.FirstOrDefault(j => j.Id == i.Id),
                                     RepetedIdentity = listRepetedIdentity.FirstOrDefault(k => k == i.Id)
                                 }).ToList();

        List<ProductValidateDTO> listProductValidate = listProductDelete.Select(i => new ProductValidateDTO().ValidateDelete(i.InputDeleteProduct, i.listOriginalIdentity, i.RepetedIdentity)).ToList();

        _productValidateService.ValidateDelete(listProductValidate);

        var listNotification = GetAllNotification();

        if (listNotification!.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<bool>.Failure(listNotification!);

        var listDeleteProduct = (from i in listProductValidate
                                 select i.Original).ToList();

        await _productRepository.Delete(listDeleteProduct);
        return BaseResult<bool>.Success(true, listNotification!);
    }
    #endregion

}