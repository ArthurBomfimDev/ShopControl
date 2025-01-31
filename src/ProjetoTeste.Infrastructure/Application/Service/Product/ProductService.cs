using AutoMapper;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Application.Service.Base;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.ValidateService;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class ProductService : BaseService<IProductRepository, Product, InputCreateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentityViewProduct, OutputProduct>, IProductService
{
    #region Dependency Injection
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IProductValidateService _productValidateService;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, IProductValidateService productValidateService, IMapper mapper) : base(productRepository, mapper)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _productValidateService = productValidateService;
        _mapper = mapper;
    }
    #endregion

    #region Get
    public async Task<List<OutputProduct>> GetListByBrandId(InputIdentityViewBrand inputIdentifyViewBrand)
    {
        var listByBrandId = await _productRepository.GetListByBrandId(inputIdentifyViewBrand.Id);
        return _mapper.Map<List<OutputProduct>>(listByBrandId);
    }
    #endregion

    #region Create
    public override async Task<BaseResponse<OutputProduct>> Create(InputCreateProduct inputCreateProduct)
    {
        var response = new BaseResponse<OutputProduct>();

        var createValidate = await CreateMultiple([inputCreateProduct]);

        response.Success = createValidate.Success;
        response.Message = createValidate.Message;

        if (!response.Success)
            return response;

        response.Content = createValidate.Content.FirstOrDefault();
        return response;
    }

    public override async Task<BaseResponse<List<OutputProduct>>> CreateMultiple(List<InputCreateProduct> listinputCreateProduct)
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
                              OriginalCode = listOriginalCode.FirstOrDefault(j => j.Code == i.Code),
                              RepeteCode = listRepeteCode.FirstOrDefault(k => k == i.Code),
                              BrandExists = listBrand.FirstOrDefault(l => l == i.BrandId)
                          }).ToList();

        List<ProductValidate> listProductValidate = listCreate.Select(i => new ProductValidate().ValidateCreate(i.InputCreateProduct, _mapper.Map<ProductDTO>(i.OriginalCode), i.RepeteCode, i.BrandExists)).ToList();
        var validateCreate = await _productValidateService.ValidateCreate(listProductValidate);

        response.Success = validateCreate.Success;
        response.Message = validateCreate.Message;
        if (!response.Success)
        {
            return response;
        }

        var listCreateProduct = (from i in validateCreate.Content
                                 let message = response.AddSuccessMessage($"O Produto {i.InputCreateProduct.Name} foi cadastrado com sucesso")
                                 select i.InputCreateProduct).Select(i => new Product(i.Name, i.Code, i.Description, i.Price, i.BrandId, i.Stock, default)).ToList();

        var listCreatedProduct = await _productRepository.Create(listCreateProduct);

        response.Content = _mapper.Map<List<OutputProduct>>(listCreatedProduct);
        return response;
    }
    #endregion

    #region Update
    public override async Task<BaseResponse<bool>> Update(InputIdentityUpdateProduct inputIdentityUpdateProduct)
    {
        return await UpdateMultiple([inputIdentityUpdateProduct]);
    }

    public override async Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct)
    {
        var response = new BaseResponse<bool>();

        var listOriginalIdentity = await _productRepository.GetListByListId(listInputIdentityUpdateProduct.Select(i => i.Id).ToList());
        var listRepeteIdentity = (from i in listInputIdentityUpdateProduct
                                  where listInputIdentityUpdateProduct.Count(j => j.Id == i.Id) > 1
                                  select i.Id).ToList();
        var listOriginalCode = (await _productRepository.GetListByCodeList(listInputIdentityUpdateProduct.Select(i => i.InputUpdateProduct.Code).ToList())).Select(i => i.Code);
        var listRepeteCode = (from i in listInputIdentityUpdateProduct
                              where listInputIdentityUpdateProduct.Count(j => j.InputUpdateProduct.Code == i.InputUpdateProduct.Code) > 1
                              select i.InputUpdateProduct.Code).ToList();
        var listBrand = (await _brandRepository.GetListByListId(listInputIdentityUpdateProduct.Select(i => i.InputUpdateProduct.BrandId).ToList())).Select(i => i.Id);

        var listUpdate = (from i in listInputIdentityUpdateProduct
                          select new
                          {
                              InputIdentityUpdateProduct = i,
                              OriginalIdentity = listOriginalIdentity.FirstOrDefault(j => j.Id == i.Id),
                              RepeteIdentity = listRepeteIdentity.FirstOrDefault(k => k == i.Id),
                              OriginalCode = listOriginalCode.FirstOrDefault(l => l == i.InputUpdateProduct.Code),
                              RepeteCode = listRepeteCode.FirstOrDefault(m => m == i.InputUpdateProduct.Code),
                              BrandExists = listBrand.FirstOrDefault(n => n == i.InputUpdateProduct.BrandId)
                          }).ToList();

        List<ProductValidate> listProductValidate = listUpdate.Select(i => new ProductValidate().ValidateUpdate(i.InputIdentityUpdateProduct, _mapper.Map<ProductDTO>(i.OriginalIdentity), i.OriginalCode, i.RepeteIdentity, i.RepeteCode, i.BrandExists)).ToList();
        var validateUpdate = await _productValidateService.ValidateUpdate(listProductValidate);

        response.Success = validateUpdate.Success;
        response.Message = validateUpdate.Message;
        if (!response.Success)
        {
            response.Content = false;
            return response;
        }

        var listUpdateProduct = (from i in validateUpdate.Content
                                 let name = i.Original.Name = i.InputIdentityUpdateProduct.InputUpdateProduct.Name
                                 let code = i.Original.Code = i.InputIdentityUpdateProduct.InputUpdateProduct.Code
                                 let description = i.Original.Description = i.InputIdentityUpdateProduct.InputUpdateProduct.Description
                                 let brandId = i.Original.BrandId = i.InputIdentityUpdateProduct.InputUpdateProduct.BrandId
                                 let stock = i.Original.Stock = i.InputIdentityUpdateProduct.InputUpdateProduct.Stock
                                 let Price = i.Original.Price = i.InputIdentityUpdateProduct.InputUpdateProduct.Price
                                 let message = response.AddSuccessMessage($"O Produto com Id: {i.Original.Id} foi atualizado com sucesso")
                                 select i.Original).ToList();

        response.Content = await _productRepository.Update(_mapper.Map<List<Product>>(listUpdateProduct));
        return response;
    }
    #endregion

    #region Delete
    public override async Task<BaseResponse<bool>> Delete(InputIdentityDeleteProduct inputIdentifyDeleteProduct)
    {
        return await DeleteMultiple([inputIdentifyDeleteProduct]);
    }

    public override async Task<BaseResponse<bool>> DeleteMultiple(List<InputIdentityDeleteProduct> listInputIdentifyDeleteProduct)
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

        List<ProductValidate> listProductValidate = listProductDelete.Select(i => new ProductValidate().ValidateDelete(i.InputDeleteProduct, _mapper.Map<ProductDTO>(i.listOriginalIdentity), i.RepetedIdentity)).ToList();

        var validateDelete = await _productValidateService.ValidateDelete(listProductValidate);

        response.Success = validateDelete.Success;
        response.Message = validateDelete.Message;

        if (!response.Success)
            return response;

        var listDeleteProduct = _mapper.Map<List<Product>>((from i in validateDelete.Content
                                                            let message = response.AddSuccessMessage($"O Produto: {i.Original.Name} com Id: {i.InputIdentifyDeleteProduct.Id} foi deletado com sucesso")
                                                            select i.Original).ToList());

        response.Content = await _productRepository.Delete(listDeleteProduct);
        return response;
    }
    #endregion

}