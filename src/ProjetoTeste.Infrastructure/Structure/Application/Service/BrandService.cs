using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.ValueFrameworkCore;
using ProjetoEstagioAPI.Arguments.Brands;
using ProjetoEstagioAPI.Infrastructure.UnitOfWork;
using ProjetoEstagioAPI.Mapping.Brands;
using ProjetoEstagioAPI.Models;
using System.Drawing.Drawing2D;

namespace ProjetoEstagioAPI.Services;

public class BrandService
{
    private readonly IUnitOfWork _unitOfWork;

    public BrandService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response<List<OutputBrand>>> GetAll()
    {
        var brandList = await _unitOfWork.BrandRepository.GetAllAsync();
        if (brandList is null || brandList.Count() == 0)
        {
            return new Response<List<OutputBrand>>
            {
                Message = " >>> Não há Marcas Cadastradas No Sistema <<<",
                Success = false,
            };
        }
        return new Response<List<OutputBrand>>
        {
            Success = true,
            Value = brandList.ToOutputBrandList(),
        };
    }
    public async Task<Response<OutputBrand>> Get(long id)
    {
        var brand = await _unitOfWork.BrandRepository.Get(id);
        if (brand is null)
        {
            return new Response<OutputBrand> { Message = " >>> Marca com o Id digitado NÃO encontrada <<<", Success = false };
        }
        return new Response<OutputBrand>
        {
            Value = brand.ToOutputBrand(),
            Success = true,
        };
    }
    public async Task<Response<OutputBrand>> Create(InputCreateBrand input)
    {
        //var nameExists =  await _unitOfWork.BrandRepository.Exist(input.Name);
        //if (nameExists is false)
        //{
        //    return new Response<OutputBrand> { Message = " >>> ERRO - Nome de Marca já existe <<<", Success = false };
        //}
        //var CodeExists = await _unitOfWork.BrandRepository.Exist(input.Code);
        //if (CodeExists is false)
        //{
        //    return new Response<OutputBrand> { Message = " >>> Erro - Codigo de Marca já cadastrado <<<", Success = false };
        //}
        var createBrand = await _unitOfWork.BrandRepository.Create(input.ToBrand());
        //if (createBrand is null)
        //{
        //    return new Response<OutputBrand> { Message = " >>> ERRO - Marca não criada - Dados digitados errados ou incompletos <<<", Success = false };
        //}
        await _unitOfWork.Commit();
        return new Response<OutputBrand>
        {
            Value = createBrand.ToOutputBrand(),
            Success = true,
        };
    }
    public async Task<Response<OutputBrand>> Update(long id, InputUpdateBrand brand)
    {
        var brandExists = await _unitOfWork.BrandRepository.Get(id);

        if (brand is null)
        {
            return new Response<OutputBrand> { Message = " >>> Marca com o Id digitado NÃO encontrada <<<", Success = false };
        }
        brandExists.Name = brand.Name;
        brandExists.Code = brand.Code;
        brandExists.Description = brand.Description;
        var brandUpdate = _unitOfWork.BrandRepository.Update(brandExists);
        if (brandUpdate is null)
        {
            return new Response<OutputBrand> { Message = " >>> ERRO - Marca não atualizada - Dados digitados errados ou incompletos <<<", Success = false };
        }
        await _unitOfWork.Commit();
        return new Response<OutputBrand> { Success = true, Message = " >>> Marca Atualizada com SUCESSO <<<" };
    }
    public async Task<Response<OutputBrand>> Delete(long id)
    {
        var brandExists = await _unitOfWork.BrandRepository.Get(id);

        if (brandExists is null)
        {
            return new Response<OutputBrand> { Message = " >>> Marca com o Id digitado NÃO encontrada <<<", Success = false };
        }
        var brandDelete = _unitOfWork.BrandRepository.Delete(id);
        if (brandDelete is null)
        {
            return new Response<OutputBrand> { Message = " >>> ERRO - Marca não apagada - Dados digitados errados ou incompletos <<<", Success = false };
        }
        await _unitOfWork.Commit();
        return new Response<OutputBrand> { Message = " >>> Marca DELETADA com SUCESSO <<<", Success = true };
    }
}