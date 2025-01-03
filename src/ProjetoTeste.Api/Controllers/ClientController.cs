using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Client;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Api.Controllers;

public class ClientController : BaseController
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService, IUnitOfWork _unitOfWork) : base(_unitOfWork)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OutputClient>>> GetAll()
    {
        var get = await _clientService.GetAll();
        return Ok(get.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(long id)
    {
        var get = await _clientService.Get(id);
        return Ok(get.Value);
    }

    [HttpPost]
    public async Task<ActionResult> Create(InputCreateClient input)
    {
        Response<OutputClient> createClient = await _clientService.Create(input);
        if (!createClient.Success)
        {
            return BadRequest(createClient.Message);
        }
        return Ok(createClient.Value);
    }

    [HttpPut]
    public async Task<ActionResult> Update(long Id, InputUpdateClient input)
    {
        Response<bool> update = await _clientService.Update(Id, input);
        if (!update.Success)
        {
            return BadRequest(update.Message);
        }
        return Ok(update.Message);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(long id)
    {
        Response<bool> delete = await _clientService.Delete(id);
        if (!delete.Success)
        {
            return BadRequest(delete.Message);
        }
        return Ok(delete.Message);
    }
}