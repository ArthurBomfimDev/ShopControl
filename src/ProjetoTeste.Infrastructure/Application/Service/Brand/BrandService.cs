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

    // Tirar o validateCreate
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

    public async Task<List<OutputBrand>> GetListByListId(List<long> ids)
    {
        var brand = await _brandRepository.GetListByListId(ids);
        return (from i in brand
                select i.ToOutputBrand()).ToList();
    }

    // colocar no product
    //public async Task<List<OutputBrand>> GetAllAndProduct()
    //{
    //    var brandListwithProducts = await _brandRepository.GetAllAndProduct();
    //    return (from i in brandListwithProducts select i.ToOutputBrand()).ToList();
    //}

    //public async Task<List<OutputBrand>> GetAndProduct(long id)
    //{
    //    var brandListwithProducts = await _brandRepository.GetAndProduct(id);
    //    return (from i in brandListwithProducts select i.ToOutputBrand()).ToList();
    //}

    public async Task<BaseResponse<List<OutputBrand>>> Create(List<InputCreateBrand> input)
    {
        var validateCreate = await _brandValidadeService.ValidateCreate(input);
        var response = new BaseResponse<List<OutputBrand>>() { Message = validateCreate.Message };
        if (!validateCreate.Success)
        {
            response.Success = false;
            return response;
        }

        var brand = (from i in validateCreate.Content
                     select new Brand(i.Name, i.Code, i.Description, default)).ToList();

        var createBrand = await _brandRepository.Create(brand);

        if (createBrand.Count() == 0)
        {
            response.AddSuccessMessage(" >>> ERRO - Marca não criada - Dados digitados errados ou incompletos <<<");
            return response;
        }

        response.Content = (from i in createBrand select i.ToOutputBrand()).ToList();
        return response;
    }

    public async Task<BaseResponse<bool>> Update(List<long> ids, List<InputUpdateBrand> brand)
    {
        var validateUpdate = await _brandValidadeService.ValidateUpdate(ids, brand);
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

    public async Task<BaseResponse<bool>> Delete(List<long> ids)
    {
        var validateCreate = await _brandValidadeService.ValidadeDelete(ids);
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

}