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

    // Tirar o response
    public async Task<List<OutputBrand>> GetAll()
    {
        var brandList = await _brandRepository.GetAllAsync();
        return (from i in brandList select i.ToOutputBrand()).ToList();
    }

    public async Task<List<OutputBrand>> Get(List<long> ids)
    {
        var brand = await _brandRepository.Get(ids);
        return (from i in brand
                select i.ToOutputBrand()).ToList();
    }

    public async Task<List<OutputBrand>> GetAllAndProduct()
    {
        var brandListwithProducts = await _brandRepository.GetAllAndProduct();
        return (from i in brandListwithProducts select i.ToOutputBrand()).ToList();
    }

    public async Task<List<OutputBrand>> GetAndProduct(long id)
    {
        var brandListwithProducts = await _brandRepository.GetAndProduct(id);
        return (from i in brandListwithProducts select i.ToOutputBrand()).ToList();
    }

    //public async Task<BaseResponse<ProjetoTeste.Infrastructure.Persistence.Entity.Brand>> BrandExists(long id)
    //{
    //    var brand = await _brandRepository.Get(id);
    //    if (brand == null)
    //    {
    //        return new BaseResponse<ProjetoTeste.Infrastructure.Persistence.Entity.Brand>
    //        {
    //            Success = false,
    //            Message = { " >>> Marca com o Id digitado NÃO encontrada <<<" }
    //        };
    //    }
    //    return new BaseResponse<ProjetoTeste.Infrastructure.Persistence.Entity.Brand>
    //    {
    //        Content = brand,
    //        Success = true,
    //    };
    //}

    public async Task<BaseResponse<List<OutputBrand>>> Create(List<InputCreateBrand> input)
    {
        var response = await _brandValidadeService.ValidateCreate(input);
        if (!response.Success)
        {
            return new BaseResponse<List<OutputBrand>>() { Success = false, Message = response.Message };
        }

        var brand = (from i in response.Content
                     select new Brand(i.Name, i.Code, i.Description, default)).ToList();

        var createBrand = await _brandRepository.Create(brand);

        if (createBrand.Count() == 0)
        {
            response.Message.Add(new Notification { Message = " >>> ERRO - Marca não criada - Dados digitados errados ou incompletos <<<", Type = EnumNotificationType.Error });
            return new BaseResponse<List<OutputBrand>>() { Success = false, Message = response.Message };
        }

        return new BaseResponse<List<OutputBrand>>
        {
            Content = (from i in createBrand select i.ToOutputBrand()).ToList(),
            Success = true,
            Message = response.Message
        };
    }

    public async Task<BaseResponse<bool>> Update(List<long> ids, List<InputUpdateBrand> brand)
    {
        var brandUpdateList = await _brandValidadeService.ValidateUpdate(ids, brand);
        if (!brandUpdateList.Success)
        {
            return new BaseResponse<bool>() { Success = false, Message = brandUpdateList.Message };
        }
        var brandUpdate = await _brandRepository.Update(brandUpdateList.Content);

        if (!brandUpdate)
        {
            var erro = new BaseResponse<bool>() { Success = false, Message = brandUpdateList.Message };
            erro.Message.Add(new Notification { Message = " >>> Não foi possivel atualizar a Marca <<<", Type = EnumNotificationType.Error });
            return erro;
        }

        foreach (var item in brandUpdateList.Content)
        {
            brandUpdateList.Message.Add(new Notification { Message = $" >>> A Marca: {item.Name} com Id: {item.Id} foi Atualizada com SUCESSO <<<", Type = EnumNotificationType.Success });
        }
        return new BaseResponse<bool> { Success = true, Message = brandUpdateList.Message };
    }

    public async Task<BaseResponse<bool>> Delete(List<long> ids)
    {
        var response = await _brandValidadeService.ValidadeDelete(ids);
        return response;
    }
}