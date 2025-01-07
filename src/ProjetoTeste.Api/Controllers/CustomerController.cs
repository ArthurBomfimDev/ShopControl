using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Api.Controllers;

public class CustomerController : BaseController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService, IUnitOfWork _unitOfWork) : base(_unitOfWork)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OutputCustomer>>> GetAll()
    {
        var get = await _customerService.GetAll();
        return Ok(get.Content);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(long id)
    {
        var get = await _customerService.Get(id);
        return Ok(get.Content);
    }

    [HttpPost]
    public async Task<ActionResult> Create(InputCreateCustomer input)
    {
        BaseResponse<OutputCustomer> createClient = await _customerService.Create(input);
        if (!createClient.Success)
        {
            return BadRequest(createClient.Message);
        }
        return Ok(createClient.Content);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(long Id, InputUpdateCustomer input)
    {
        BaseResponse<bool> update = await _customerService.Update(Id, input);
        if (!update.Success)
        {
            return BadRequest(update.Message);
        }
        return Ok(update.Message);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        BaseResponse<bool> delete = await _customerService.Delete(id);
        if (!delete.Success)
        {
            return BadRequest(delete.Message);
        }
        return Ok(delete.Message);
    }
}