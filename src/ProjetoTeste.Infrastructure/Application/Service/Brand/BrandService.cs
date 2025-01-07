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

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    // Tirar o response
    public async Task<List<OutputBrand>> GetAll()
    {
        var brandList = await _brandRepository.GetAllAsync();
        return (from i in brandList select i.ToOutputBrand()).ToList();
    }

    public async Task<OutputBrand> Get(long id)
    {
        var brand = await _brandRepository.Get(id);
        return brand.ToOutputBrand();
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

    public async Task<BaseResponse<ProjetoTeste.Infrastructure.Persistence.Entity.Brand>> BrandExists(long id)
    {
        var brand = await _brandRepository.Get(id);
        if (brand == null)
        {
            return new BaseResponse<ProjetoTeste.Infrastructure.Persistence.Entity.Brand>
            {
                Success = false,
                Message = { " >>> Marca com o Id digitado NÃO encontrada <<<" }
            };
        }
        return new BaseResponse<ProjetoTeste.Infrastructure.Persistence.Entity.Brand>
        {
            Content = brand,
            Success = true,
        };
    }

    // Seprar as validações
    public async Task<BaseResponse<OutputBrand>> Create(List<InputCreateBrand> input)
    {
        if (input.Count() == 0)
        {
            return new BaseResponse<OutputBrand> { Message = { " >>> Dados Inseridos Inválidos <<<" }, Success = false };
        }
        var response = new BaseResponse<OutputBrand>();
        // Trocar para lista de cod
        var CodeExists = await _brandRepository.Exist(input.);
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
        return new BaseResponse<OutputBrand>
        {
            Content = createBrand.ToOutputBrand(),
            Success = true,
        };
    }

    public async Task<BaseResponse<bool>> Update(long id, InputUpdateBrand brand)
    {
        var response = await BrandExists(id);
        var brandExists = response.Content;

        if (brand is null)
        {
            response.Success = false;
            response.Message.Add(" >>> Dados Inseridos Inválidos <<<");
        }
        if (brandExists is null)
        {
            return new BaseResponse<bool> { Message = response.Message, Success = false };
        }
        var codeExists = await _brandRepository.Exist(brand.Code);
        if (brandExists.Code != brand.Code && codeExists)
        {
            response.Message.Add(" >>> Código não pode ser Alterado - Em Uso por outra Marca <<<");
        }
        if (!response.Success)
        {
            return new BaseResponse<bool>() { Message = response.Message, Success = false };
        }
        brandExists.Name = brand.Name;
        brandExists.Code = brand.Code;
        brandExists.Description = brand.Description;
        var brandUpdate = _brandRepository.Update(brandExists);
        if (brandUpdate is null)
        {
            response.Success = false;
            response.Message.Add(" >>> ERRO - Marca não atualizada - Dados digitados errados ou incompletos <<<");
        }
        if (!response.Success)
        {
            return new BaseResponse<bool> { Success = false, Message = response.Message };
        }
        return new BaseResponse<bool> { Success = true, Message = { " >>> Marca Atualizada com SUCESSO <<<" } };
    }

    public async Task<BaseResponse<bool>> Delete(long id)
    {
        var response = await BrandExists(id);
        var brandExists = response.Content;
        if (brandExists is null)
        {
            return new BaseResponse<bool> { Success = false, Message = response.Message };
        }
        bool brandDelete = await _brandRepository.Delete(id);
        if (!brandDelete)
        {
            response.Success = false;
            response.Message.Add(" >>> ERRO - Marca não apagada - Dados digitados errados ou incompletos <<<");
        }
        return new BaseResponse<bool> { Message = { " >>> Marca DELETADA com SUCESSO <<<" }, Success = true };
    }
}