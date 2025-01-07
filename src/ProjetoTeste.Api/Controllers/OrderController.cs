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
        return Ok(get.Content);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<OutputOrder>>> Get(long id)
    {
        var get = await _orderService.Get(id);
        return Ok(get.Content);
    }

    [HttpGet("BestSellerProduct")]
    public async Task<ActionResult<OutputSellProduct>> GetBestSeller()
    {
        var order = await _orderService.BestSellerProduct();
        if (!order.Success) return BadRequest(order.Message);
        return Ok(order.Content);
    }

    [HttpGet("LesatSoldProduct")]
    public async Task<ActionResult<OutputSellProduct>> LesatSoldProduct()
    {
        var order = await _orderService.LeastSoldProduct();
        if (!order.Success) return BadRequest(order.Message);
        return Ok(order.Content);
    }

    [HttpGet("BestSellingProducts")]
    public async Task<ActionResult<OutputSellProduct>> GetBestSellers()
    {
        var order = await _orderService.TopSellers();
        if (!order.Success) return BadRequest(order.Message);
        return Ok(order.Content);
    }

    [HttpGet("BiggestBuyer/Quantity")]
    public async Task<ActionResult<OutputCustomerOrder>> BiggestBuyer()
    {
        var client = await _orderService.BiggestBuyer();
        if (!client.Success) return BadRequest(client.Message);
        return Ok(client.Content);
    }

    [HttpGet("BiggestBuyer/Price")]
    public async Task<ActionResult<OutputCustomerOrder>> BiggestBuyerPrice()
    {
        var client = await _orderService.BiggestBuyerPrice();
        if (!client.Success) return BadRequest(client.Message);
        return Ok(client.Content);
    }

    [HttpGet("BrandBestSeller")]
    public async Task<ActionResult<OutputBrandBestSeller>> Brand()
    {
        var brand = await _orderService.BrandBestSeller();
        if (!brand.Success) return BadRequest(brand.Message);
        return Ok(brand.Content);
    }
    [HttpGet("MostAvarege")]
    public async Task<ActionResult> MostAvarege()
    {
        var avarage = await _orderService.Avarege();
        if (!avarage.Success) return BadRequest(avarage.Message);
        var response = "Maior Média de vendas: " + avarage.Content;
        return Ok(response);
    }

    [HttpGet("Total")]
    public async Task<ActionResult> Total()
    {
        var total = await _orderService.Total();
        if (!total.Success) return BadRequest(total.Message);
        return Ok(total.Content);
    }

    [HttpPost]
    public async Task<ActionResult<OutputOrder>> Create(InputCreateOrder input)
    {
        var create = await _orderService.Create(input);
        if (!create.Success)
        {
            return BadRequest(create.Message);
        }
        return Ok(create.Content);
    }

    [HttpPost("Add")]
    public async Task<ActionResult<OutputOrder>> Add(InputCreateProductOrder input)
    {
        var add = await _orderService.CreateProductOrder(input);
        if (!add.Success)
        {
            return BadRequest(add.Message);
        }
        return Ok(add.Content);
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