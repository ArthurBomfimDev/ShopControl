using ProjetoTeste.Arguments.Arguments.Brands;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Application.Service;

public class BrandService :IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }
    public async Task<Response<List<OutputBrand>>> GetAll()
    {
        var brandList = await _brandRepository.GetAllAsync();
        return new Response<List<OutputBrand>>
        {
            Success = true,
            Value = brandList.ToOutputBrandList(),
        };
    }
    public async Task<Response<OutputBrand>> Get(long id)
    {
        var brand = await _brandRepository.Get(id);
        return new Response<OutputBrand>
        {
            Value = brand.ToOutputBrand(),
            Success = true,
        };
    }
    public async Task<Response<Brand>> BrandExists(long id)
    {
        var brand = await _brandRepository.Get(id);
        if (brand == null)
        {
            return new Response<Brand>
            {
                Success = false,
                Message = { " >>> Marca com o Id digitado NÃO encontrada <<<" }
            };
        }
        return new Response<Brand>
        {
            Value = brand,
            Success = true,
        };
    }
    public async Task<Response<OutputBrand>> Create(InputCreateBrand input)
    {
        if (input is null)
        {
            return new Response<OutputBrand> { Message = { " >>> Dados Inseridos Inválidos <<<" }, Success = false };
        }
        var response = new Response<OutputBrand>();
        var CodeExists = await _brandRepository.Exist(input.Code);
        if (CodeExists is false)
        {
            response.Message.Add(" >>> Erro - Codigo de Marca já cadastrado <<<");
            response.Success = false;
        }
        var createBrand = await _brandRepository.Create(input.ToBrand());
        if (createBrand is null)
        {
            response.Message.Add(" >>> ERRO - Marca não criada - Dados digitados errados ou incompletos <<<");
            response.Success = false;
        }
        return new Response<OutputBrand>
        {
            Value = createBrand.ToOutputBrand(),
            Success = true,
        };
    }
    public async Task<Response<bool>> Update(long id, InputUpdateBrand brand)
    {
        var response = await BrandExists(id);
        var brandExists = response.Value;

        if (brand is null)
        {
            response.Success = false;
            response.Message.Add(" >>> Dados Inseridos Inválidos <<<");
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
            return new Response<bool> { Success = false, Message = response.Message };
        }
        return new Response<bool> { Success = true, Message = { " >>> Marca Atualizada com SUCESSO <<<" } };
    }
    public async Task<Response<bool>> Delete(long id)
    {
        var response = await BrandExists(id);
        var brandExists = response.Value;
        var brandDelete = _brandRepository.Delete(id);
        if (brandDelete is null)
        {
            response.Success = false;
            response.Message.Add(" >>> ERRO - Marca não apagada - Dados digitados errados ou incompletos <<<");
        }
        return new Response<bool> { Message = { " >>> Marca DELETADA com SUCESSO <<<" }, Success = true };
    }
}