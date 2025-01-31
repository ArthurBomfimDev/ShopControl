using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
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

    [HttpPost("id")]
    public async Task<ActionResult<List<OutputOrder>>> Get(InputIdentifyViewOrder inputIdentifyViewOrder)
    {
        var get = await _orderService.Get(inputIdentifyViewOrder);
        return Ok(get.Content);
    }

    [HttpPost("Id/Multiple")]
    public async Task<ActionResult<List<OutputOrder>>> Get(List<InputIdentifyViewOrder> listInputIdentifyViewOrder)
    {
        var get = await _orderService.GetListByListId(listInputIdentifyViewOrder);
        return Ok(get.Content);
    }

    [HttpGet("BestSellerProduct")]
    public async Task<ActionResult<OutputMaxSaleValueProduct>> GetBestSeller()
    {
        var order = await _orderService.BestSellerProduct();
        return Ok(order);
    }

    [HttpGet("LesatSoldProduct")]
    public async Task<ActionResult<OutputMaxSaleValueProduct>> LesatSoldProduct()
    {
        var order = await _orderService.LeastSoldProduct();
        return Ok(order);
    }

    [HttpGet("GetMostOrderedProduct")]
    public async Task<ActionResult<OutputMaxSaleValueProduct>> GetBestSellers()
    {
        var order = await _orderService.GetMostOrderedProduct();
        return Ok(order);
    }

    [HttpGet("BiggestBuyer")]
    public async Task<ActionResult<OutputCustomerOrder>> BiggestBuyer()
    {
        var client = await _orderService.BiggestBuyer();
        return Ok(client);
    }

    [HttpGet("BrandBestSeller")]
    public async Task<ActionResult<OutputBrandBestSeller>> Brand()
    {
        var brand = await _orderService.BrandBestSeller();
        return Ok(brand);
    }

    [HttpGet("HighestAverageSalesValue")]
    public async Task<ActionResult> HighestAverageSalesValue()
    {
        var avarage = await _orderService.HighestAverageSalesValue();
        return Ok(avarage);
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
        return Ok(create.Content);
    }

    [HttpPost("Multiple")]
    public async Task<ActionResult<OutputOrder>> Create(List<InputCreateOrder> listInputCreateOrder)
    {
        var create = await _orderService.CreateMultiple(listInputCreateOrder);
        if (!create.Success)
        {
            return BadRequest(create);
        }
        return Ok(create);
    }

    [HttpPost("CreateProductOrder")]
    public async Task<ActionResult<OutputOrder>> CreateProductOrder(InputCreateProductOrder input)
    {
        var add = await _orderService.CreateProductOrder(input);
        if (!add.Success)
        {
            return BadRequest(add);
        }
        return Ok(add);
    }

    [HttpPost("CreateProductOrder/Multiple")]
    public async Task<ActionResult<OutputOrder>> CreateProductOrder(List<InputCreateProductOrder> listInputCreateProductOrder)
    {
        var add = await _orderService.CreateProductOrderMultiple(listInputCreateProductOrder);
        if (!add.Success)
        {
            return BadRequest(add);
        }
        return Ok(add);
    }
}