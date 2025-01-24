using Microsoft.AspNetCore.Mvc; // utilizando as classes, interfaces e métodos contidos no namespace Microsoft.AspNetCore.Mvc.--> Importa as funções do fremework dotnet mvc(utilizado para aplicações web) tem os métodos como controllers endpoints etc
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Api.Controllers;

// Um framework é um conjunto de ferramentas, bibliotecas e diretrizes projetadas para ajudar desenvolvedores a construir aplicações de forma mais eficiente e estruturada. Ele fornece uma base pronta que resolve problemas comuns, permitindo que os programadores se concentrem nas partes específicas do sistema que estão desenvolvendo.
// Base pronto
// Padrões
// Funções e métodos
// Reutilização
public class BrandController : BaseController
{
    //Injeção de dependencia
    private readonly IBrandService _brandService;

    //Preenchidas e fornecidas pelo mvc

    public BrandController(IUnitOfWork unitOfWork, IBrandService brandService) : base(unitOfWork) // Injeção de dependencias (unitOfWork) garamte a trasação com o banco de dados
    {
        _brandService = brandService; // Contém a logica e a manipulação de dados
    }
    //Para desacoplar lógica, centralizar transações e facilitar manutenção e testes.

    [HttpGet]
    public async Task<ActionResult<List<OutputBrand?>>> GetAll() // ActionResult -> é um modelo genérico e flexível, serve para padronizar o retorno de respostas HTTP (polimorfismo) -> actionsResult classe base e genmerica herdada por object result q por sua vez é herdada por Ok, badrequest etc...
    {
        var brandList = await _brandService.GetAll();
        return Ok(brandList);
    }

    [HttpGet("Id")]
    public async Task<ActionResult<OutputBrand?>> Get(InputIdentifyViewBrand inputIdentifyViewBrand)
    {
        var brand = await _brandService.Get(inputIdentifyViewBrand);
        return Ok(brand);
    }

    [HttpPost("Id/Multiple")]
    public async Task<ActionResult<List<OutputBrand>?>> GetListByListId(List<InputIdentifyViewBrand> listInputIdentifyViewBrand)
    {
        var brand = await _brandService.GetListByListId(listInputIdentifyViewBrand);
        return Ok(brand);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<List<OutputBrand>>>> Create(InputCreateBrand inputCreateBrand)
    {
        var createdBrand = await _brandService.Create(inputCreateBrand);
        if (!createdBrand.Success)
        {
            return BadRequest(createdBrand);
        }
        return Ok(createdBrand);
    }

    [HttpPost("Multiple")]
    public async Task<ActionResult<BaseResponse<List<OutputBrand>>>> Create(List<InputCreateBrand> listInputCreateBrand)
    {
        var createdBrand = await _brandService.CreateMultiple(listInputCreateBrand);
        if (!createdBrand.Success)
        {
            return BadRequest(createdBrand);
        }
        return Ok(createdBrand);
    }

    [HttpPut]
    public async Task<ActionResult<BaseResponse<bool>>> Update(InputIdentityUpdateBrand InputIdentityUpdateBrand)
    {
        var updateBrand = await _brandService.Update(InputIdentityUpdateBrand);
        if (!updateBrand.Success)
        {
            return BadRequest(updateBrand);
        }
        return Ok(updateBrand);
    }

    [HttpPut("Multiple")]
    public async Task<ActionResult<BaseResponse<bool>>> Update(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        var updateBrand = await _brandService.UpdateMultiple(listInputIdentityUpdateBrand);
        if (!updateBrand.Success)
        {
            return BadRequest(updateBrand);
        }
        return Ok(updateBrand);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(InputIdentifyDeleteBrand inputIdentifyDeleteBrand)
    {
        var deleteBrand = await _brandService.Delete(inputIdentifyDeleteBrand);
        if (!deleteBrand.Success)
        {
            return BadRequest(deleteBrand);
        }
        return Ok(deleteBrand);
    }

    [HttpDelete("Multiple")]
    public async Task<ActionResult> Delete(List<InputIdentifyDeleteBrand> listInputIdentifyDeleteBrand)
    {
        var deleteBrand = await _brandService.DeleteMultiple(listInputIdentifyDeleteBrand);
        if (!deleteBrand.Success)
        {
            return BadRequest(deleteBrand);
        }
        return Ok(deleteBrand);
    }
}