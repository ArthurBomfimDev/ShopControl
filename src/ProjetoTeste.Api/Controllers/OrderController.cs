using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductsOrder;
using ProjetoTeste.Infrastructure.Application.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Interface.Service;
using System.Security.Cryptography.X509Certificates;
namespace ProjetoTeste.Api.Controllers;

public class OrderController : BaseController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService, IUnitOfWork _unitOfWork) : base(_unitOfWork)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OutputOrder>>> GetAll()
    {
        var get = await _orderService.GetAll();
        return Ok(get.Value);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<List<OutputOrder>>> Get(long id)
    {
        var get = await _orderService.Get(id);
        return Ok(get.Value);
    }
    [HttpPost]
    public async Task<ActionResult<OutputOrder>> Create(InputCreateOrder input)
    {
        var create =  await _orderService.Create(input);
        if (!create.Success)
        {
            return BadRequest(create.Message);
        }
        return Ok(create.Value);
    }
    [HttpPost("Add")]
    public async Task<ActionResult<OutputOrder>> Add(InputCreateProductOrder input)
    {
        var add = await _orderService.Add(input);
        if (!add.Success)
        {
            return BadRequest(add.Message);
        }
        return Ok(add.Value);
    }
    //[HttpPut]
    //public async Task<ActionResult> Update(long id, InputUpdateOrder input)
    //{
    //    var update = await _orderService.Update
    //}
    [HttpDelete]
    public async Task<ActionResult> Delete(long id)
    {
        var delete = await _orderService.Delete(id);
        if (!delete.Success)
        {
            return BadRequest(delete.Message);
        }
        return Ok(delete.Message);
    }
}
