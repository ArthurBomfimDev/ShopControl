using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ProductValidateService _productValidateService;

    public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, ProductValidateService productValidateService)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _productValidateService = productValidateService;
    }

    #region Get
    public async Task<List<OutputProduct>> GetAll()
    {
        var productList = await _productRepository.GetAll();
        return (from i in productList
                select i.ToOutputProduct()).ToList();
    }

    public async Task<OutputProduct> Get(long id)
    {
        var product = await _productRepository.Get(id);
        return product.ToOutputProduct();
    }

    public async Task<List<OutputProduct>> GetListByListId(List<long> idList)
    {
        var product = await _productRepository.GetListByListId(idList);
        return (from i in product
                select i.ToOutputProduct()).ToList();
    }

    public async Task<List<OutputProduct>> GetListByBrandId(long id)
    {
        var listByBrandId = await _productRepository.GetListByBrandId(id);
        return (from i in listByBrandId select i.ToOutputProduct()).ToList();
    }
    #endregion

    #region Create
    public async Task<BaseResponse<OutputProduct>> Create(InputCreateProduct inputCreateProduct)
    {
        var response = new BaseResponse<OutputProduct>();
        var createValidate = await CreateMultiple([inputCreateProduct]);
        response.Success = createValidate.Success;
        response.Message = createValidate.Message;
        response.Content = createValidate.Content.FirstOrDefault();
        return response;
    }

    public async Task<BaseResponse<List<OutputProduct>>> CreateMultiple(List<InputCreateProduct> listinputCreateProduct)
    {
        var response = new BaseResponse<List<OutputProduct>>();
        var listOriginalCode = await _productRepository.GetListByCodeList(listinputCreateProduct.Select(i => i.Code).ToList());
        var listRepeteCode = (from i in listinputCreateProduct
                              where listinputCreateProduct.Count(j => j.Code == i.Code) > 1
                              select i.Code).ToList();
        var listBrand = (await _brandRepository.GetListByListId(listinputCreateProduct.Select(i => i.BrandId).ToList())).Select(i => i.Id);
        var listCreate = (from i in listinputCreateProduct
                          select new
                          {
                              InputCreateProduct = i,
                              OriginalCode = listOriginalCode.FirstOrDefault(j => j.Code == i.Code).ToProductDTO(),
                              RepeteCode = listRepeteCode.FirstOrDefault(k => k == i.Code),
                              BrandExists = listBrand.FirstOrDefault(l => l == i.BrandId)
                          }).ToList();
        List<ProductValidate> listProductValidate = listCreate.Select(i => new ProductValidate().ValidateCreate(i.InputCreateProduct, i.OriginalCode, i.RepeteCode, i.BrandExists)).ToList();
        var validateCreate = await _productValidateService.ValidateCreate(listProductValidate);

        response.Success = validateCreate.Success;
        response.Message = validateCreate.Message;
        if (!response.Success)
        {
            return response;
        }

        var listCreateProduct = (from i in validateCreate.Content
                                 select i.InputCreateProduct).Select(i => new Product(i.Name, i.Code, i.Description, i.Price, i.BrandId, i.Stock, default)).ToList();
        var listCreatedProduct = await _productRepository.Create(listCreateProduct);
        response.Content = listCreatedProduct.Select(i => i.ToOutputProduct()).ToList();
        return response;
    }
    #endregion

    #region Update
    public async Task<BaseResponse<bool>> Update(InputIdentityUpdateProduct inputIdentityUpdateProduct)
    {
        return await UpdateMultiple([inputIdentityUpdateProduct]);
    }

    public async Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct)
    {
        var response = new BaseResponse<bool>();
        var listOriginalIdentity = await _productRepository.GetListByListId(listInputIdentityUpdateProduct.Select(i => i.Id).ToList());
        var listRepeteIdentity = (from i in listInputIdentityUpdateProduct
                                  where listInputIdentityUpdateProduct.Count(j => j.Id == i.Id) > 1
                                  select i.Id).ToList();
        var listOriginalCode = await _productRepository.GetListByCodeList(listInputIdentityUpdateProduct.Select(i => i.InputUpdateProduct.Code).ToList());
        var listRepeteCode = (from i in listInputIdentityUpdateProduct
                              where listInputIdentityUpdateProduct.Count(j => j.InputUpdateProduct.Code == i.InputUpdateProduct.Code) > 1
                              select i.InputUpdateProduct.Code).ToList();
        var listBrand = (await _brandRepository.GetListByListId(listInputIdentityUpdateProduct.Select(i => i.InputUpdateProduct.BrandId).ToList())).Select(i => i.Id);
        var listUpdate = (from i in listInputIdentityUpdateProduct
                          select new
                          {
                              InputIdentityUpdateProduct = i,
                              OriginalIdentity = listOriginalIdentity.FirstOrDefault(j => j.Id == i.Id).ToProductDTO(),
                              RepeteIdentity = listRepeteIdentity.FirstOrDefault(k => k == i.Id),
                              OriginalCode = listOriginalCode.FirstOrDefault(l => l.Code == i.InputUpdateProduct.Code).ToProductDTO(),
                              RepeteCode = listRepeteCode.FirstOrDefault(m => m == i.InputUpdateProduct.Code),
                              BrandExists = listBrand.FirstOrDefault(n => n == i.InputUpdateProduct.BrandId)
                          }).ToList();
        List<ProductValidate> listProductValidate = listUpdate.Select(i => new ProductValidate().ValidateUpdate(i.InputIdentityUpdateProduct, i.OriginalIdentity, i.OriginalCode, i.RepeteIdentity, i.RepeteCode, i.BrandExists)).ToList();
        var validateUpdate = await _productValidateService.ValidateUpdate(listProductValidate);

        response.Success = validateUpdate.Success;
        response.Message = validateUpdate.Message;
        if (!response.Success)
        {
            response.Content = false;
            return response;
        }

        var listOlderProduct = await _productRepository.GetListByListId(validateUpdate.Content.Select(i => i.InputIdentityUpdateProduct.Id).ToList());

        for (var i = 0; i < listOlderProduct.Count; i++)
        {
            listOlderProduct[i].Name = validateUpdate.Content[i].InputIdentityUpdateProduct.InputUpdateProduct.Name;
            listOlderProduct[i].Code = validateUpdate.Content[i].InputIdentityUpdateProduct.InputUpdateProduct.Code;
            listOlderProduct[i].Description = validateUpdate.Content[i].InputIdentityUpdateProduct.InputUpdateProduct.Name;
            listOlderProduct[i].BrandId = validateUpdate.Content[i].InputIdentityUpdateProduct.InputUpdateProduct.BrandId;
            listOlderProduct[i].Stock = validateUpdate.Content[i].InputIdentityUpdateProduct.InputUpdateProduct.Stock;
            listOlderProduct[i].Price = validateUpdate.Content[i].InputIdentityUpdateProduct.InputUpdateProduct.Price;
        }

        response.Content = await _productRepository.Update(listOlderProduct);
        return response;
    }
    #endregion

    #region Delete
    public async Task<BaseResponse<bool>> Delete(long id)
    {
        return await DeleteMultiple([id]);
    }

    public async Task<BaseResponse<bool>> DeleteMultiple(List<long> idList)
    {
        var response = new BaseResponse<bool>();
        var listOriginalIdentity = await _productRepository.GetListByListId(idList);
        var listProductDelete = (from i in idList
                                 select new
                                 {
                                     InputDeleteProduct = i,
                                     listOriginalIdentity = listOriginalIdentity.FirstOrDefault(j => j.Id == i).ToProductDTO(),
                                 }).ToList();

        List<ProductValidate> listProductValidate = listProductDelete.Select(i => new ProductValidate().ValidateDelete(i.InputDeleteProduct, i.listOriginalIdentity)).ToList();
        var validateDelete = await _productValidateService.ValidateDelete(listProductValidate);
        response.Success = validateDelete.Success;
        response.Message = validateDelete.Message;
        if (!response.Success)
            return response;

        var listDelete = await _productRepository.GetListByListId(validateDelete.Content.Select(i => i.Original.Id).ToList());
        response.Content = await _productRepository.Delete(listDelete);
        return response;
    }
    #endregion

}