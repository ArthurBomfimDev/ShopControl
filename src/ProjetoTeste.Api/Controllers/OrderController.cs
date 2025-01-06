using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductsOrder;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

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

    [HttpGet("BestSellerProduct")]
    public async Task<ActionResult<OutputSellProduct>> GetBestSeller()
    {
        var order = await _orderService.BestSellerProduct();
        return Ok(order);
    }

    [HttpGet("LesatSoldProduct")]
    public async Task<ActionResult<OutputSellProduct>> LesatSoldProduct()
    {
        var order = await _orderService.LesatSoldProduct();
        return Ok(order);
    }

    [HttpGet("BestSellingProducts")]
    public async Task<ActionResult<OutputSellProduct>> GetBestSellers()
    {
        var order = await _orderService.TopSellers();
        return Ok(order);
    }

    [HttpGet("BiggestBuyer/Quantity")]
    public async Task<ActionResult<OutputCustomerOrder>> BiggestBuyer()
    {
        var client = await _orderService.BiggestBuyer();
        return Ok(client);
    }

    [HttpGet("BiggestBuyer/Price")]
    public async Task<ActionResult<OutputCustomerOrder>> BiggestBuyerPrice()
    {
        var client = await _orderService.BiggestBuyerPrice();
        return Ok(client);
    }

    [HttpGet("BrandBestSeller")]
    public async Task<ActionResult<OutputBrandBestSeller>> Brand()
    {
        var brand = await _orderService.BrandBestSeller();
        return Ok(brand);
    }
    [HttpGet("MostAvarege")]
    public async Task<ActionResult> MostAvarege()
    {
        var avarage = await _orderService.Avarege();
        var response = "Maior Média de vendas: " + avarage;
        return Ok(response);
    }

    [HttpGet("Total")]
    public async Task<ActionResult> Total()
    {
        var total = await _orderService.Total();
        return Ok(total);
    }

    [HttpPost]
    public async Task<ActionResult<OutputOrder>> Create(InputCreateOrder input)
    {
        var create = await _orderService.Create(input);
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