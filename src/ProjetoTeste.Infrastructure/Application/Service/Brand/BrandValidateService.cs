using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class BrandValidateService
{
    private readonly IBrandRepository _brandRepository;

    public BrandValidateService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BaseResponse<List<Brand>>> ValidateCreate(List<InputCreateBrand> input)
    {
        var response = new BaseResponse<OutputBrand>();
        // Trocar para lista de cod
        var CodeExists = await _brandRepository.Exist((from i in input
                                                       select i.Code).ToList());
        if (CodeExists)
        {
            response.Message.Add(" >>> Erro - Codigo de Marca já cadastrado <<<");
            response.Success = false;
            return response;
        }
        // Trocar para lista (create)
        var createBrand = await _brandRepository.Create(input.ToBrand());
        if (createBrand is null)
        {
            response.Message.Add(" >>> ERRO - Marca não criada - Dados digitados errados ou incompletos <<<");
            response.Success = false;
        }
        if (!response.Success)
        {
            return response;
        }
    }
}