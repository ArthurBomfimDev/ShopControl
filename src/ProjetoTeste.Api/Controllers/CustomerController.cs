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
        return Ok(get);
    }

    [HttpGet("Id")]
    public async Task<ActionResult> Get(long id)
    {
        var get = await _customerService.Get(id);
        return Ok(get);
    }

    [HttpGet("Id/Multiple")]
    public async Task<ActionResult> GetListByListId(List<long> idList)
    {
        var get = await _customerService.GetListByListId(idList);
        return Ok(get);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<List<OutputCustomer>>>> Create(List<InputCreateCustomer> input)
    {
        var createClient = await _customerService.Create(input);
        if (!createClient.Success)
        {
            return BadRequest(createClient.Message);
        }
        return Ok(createClient.Content);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromQuery] List<long> idList, [FromBody] List<InputUpdateCustomer> inputList)
    {
        BaseResponse<bool> update = await _customerService.Update(idList, inputList);
        if (!update.Success)
        {
            return BadRequest(update.Message);
        }
        return Ok(update.Message);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(List<long> idList)
    {
        BaseResponse<bool> delete = await _customerService.Delete(idList);
        if (!delete.Success)
        {
            return BadRequest(delete.Message);
        }
        return Ok(delete.Message);
    }
}