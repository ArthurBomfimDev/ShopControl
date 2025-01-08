using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class BrandValidateService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IProductRepository _productRepository;

    public BrandValidateService(IBrandRepository brandRepository, IProductRepository productRepository)
    {
        _brandRepository = brandRepository;
        _productRepository = productRepository;
    }

    public async Task<BaseResponse<List<InputCreateBrand>>> ValidateCreate(List<InputCreateBrand> input)
    {
        if (input.Count() == 0)
        {
            return new BaseResponse<List<InputCreateBrand>>() { Message = { " >>> Dados Inseridos Inválidos <<<" }, Success = false };
        }

        var response = new BaseResponse<List<InputCreateBrand>>();
        // validar tamanhos por exemplo code, nome description...
        var CodeExists = (from i in input
                          where _brandRepository.CodeExists(i.Code) == true
                          select i).ToList();
        foreach (var item in CodeExists)
        {
            response.Message.Add($" >>> Erro - A marca de nome: {item.Name} o código: {item.Code} não pode ser cadastrado, por já estar em uso <<<");
        }

        if (CodeExists.Count == input.Count)
        {
            response.Success = false;
            return response;
        }

        var validateCreate = (input.Except(CodeExists)).ToList();
        response.Content = validateCreate;
        return response;
    }

    public async Task<BaseResponse<List<Brand>>> ValidateUpdate(List<long> ids, List<InputUpdateBrand> input)
    {
        var response = new BaseResponse<List<Brand>>();
        if (ids.Count() != input.Count)
        {
            return new BaseResponse<List<Brand>>() { Success = false, Message = { " >>> ERRO - A Quantidade de Id's Digitados é Diferente da Quantdade de Marcas <<<" } };
        }

        var notIdExist = (from i in ids
                          where _brandRepository.BrandExists(i) == false
                          select ids.IndexOf(i)).ToList();
        if (notIdExist.Count > 0)
        {
            for (int i = 0; i < notIdExist.Count; i++)
            {
                response.Message.Add($" >>> Marca com id: {ids[notIdExist[i]]} não encontrada <<<");
                ids.Remove(ids[notIdExist[i]]);
                input.Remove(input[notIdExist[i]]);
            }
        }
        if (input.Count == 0)
        {
            response.Success = false;
            return response;
        }

        var brandList = await _brandRepository.Get(ids);

        var codeExists = (from i in input
                          let index = input.IndexOf(i)
                          where _brandRepository.CodeExists(i.Code) == true && i.Code != brandList[index].Code
                          select input.IndexOf(i)).ToList();

        if (codeExists.Count > 0)
        {
            for (int i = 0; i < codeExists.Count; i++)
            {
                response.Message.Add($" >>> Marca: {input[codeExists[i]].Name} o código: {input[codeExists[i]].Code} não pode ser alterado - Em uso por outra marca <<<");
                ids.Remove(ids[codeExists[i]]);
                input.Remove(input[codeExists[i]]);
                brandList.Remove(brandList[codeExists[i]]);
            }
        }

        if (input.Count == 0)
        {
            response.Success = false;
            return response;
        }

        for (int i = 0; i < brandList.Count() ; i++)
        {
            brandList[i].Name = input[i].Name;
            brandList[i].Code = input[i].Code;
            brandList[i].Description = input[i].Description;
        }

        response.Content = brandList;
        return response;
    }

    public async Task<BaseResponse<bool>> ValidadeDelete(List<long> ids)
    {
        var response = new BaseResponse<bool>();
        var idExist = (from i in ids
                       where _brandRepository.BrandExists(i) == false
                       select i).ToList();

        if (idExist.Count > 0)
        {
            foreach (var id in idExist)
            {
                response.Message.Add($" >>> Marca com Id: {id} não Existe <<<");
            }
            ids = (ids.Except(idExist)).ToList();
        }

        var withProduct = await _productRepository.BrandId(ids);
        if (withProduct.Count() > 0)
        {
            foreach (var id in withProduct)
            {
                response.Message.Add($" >>> Marca com id: {id} não pode ser deletada - Possui produtos <<<");
            }
        }

        List<long> withoutProduct = (ids.Except(withProduct)).ToList();
        if (withoutProduct.Count() > 0)
        {
            var brandDelete = await _brandRepository.Get(withoutProduct);
            await _brandRepository.Delete(brandDelete);
            foreach (var id in withoutProduct)
            {
                response.Message.Add($" >>> Marca com Id: {id} deletada com sucesso <<<");
            }
        }
        return response;
    }
}