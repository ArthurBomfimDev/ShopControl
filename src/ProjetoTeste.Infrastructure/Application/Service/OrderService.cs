using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductsOrder;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application.Service;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductOrderRepository _productOrderRepository;

    public OrderService(IOrderRepository orderRepository, IClientRepository clientRepository, IProductRepository productRepository, IProductOrderRepository productOrderRepository)
    {
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
        _productRepository = productRepository;
        _productOrderRepository = productOrderRepository;
    }

    public async Task<Response<List<OutputOrder>>> GetAll()
    {
        var orderList = await _orderRepository.GetProductOrders();
        var outputOrder = orderList.Select(o => new OutputOrder(o.Id, o.ClientId, default /*o.ProductOrders.ToOuputProductOrder()*/, o.Total, o.OrderDate)).ToList();
        return new Response<List<OutputOrder>> { Success = true, Value = outputOrder };
    }

    public async Task<Response<List<Order>>> Get(long id)
    {
        var order = await _orderRepository.GetProductOrdersId(id);
        return new Response<List<Order>> { Success = true, Value = order };
    }

    public async Task<Response<bool>> ValidadeteId(long id)
    {
        var orderExists = await Get(id);
        if (orderExists is null)
        {
            return new Response<bool> { Success = false, Message = { " >>> Pedido com o Id digitado NÃO encontrado <<<" } };
        }
        return new Response<bool> { Success = false };
    }

    public async Task<Response<OutputOrder>> Create(InputCreateOrder input)
    {
        var clientExists = await _clientRepository.Get(input.ClientId);
        if (clientExists is null)
        {
            return new Response<OutputOrder> { Success = false, Message = { " >>> Cliente com o Id digitado NÃO encontrado <<<" } };
        }
        var createOrder = await _orderRepository.Create(input.ToOrder());
        return new Response<OutputOrder> { Success = true, Value = createOrder.ToOutputOrder() };
    }

    public async Task<Response<OutputProductOrder>> Add(InputCreateProductOrder input)
    {
        var response = new Response<OutputProductOrder>();
        if (input is null)
        {
            return new Response<OutputProductOrder>() { Success = false, Message = { " >>> Dados Inseridos são Inválidos <<<" } };
        }
        var order = await _orderRepository.Get(input.OrderId);
        if (order is null)
        {
            response.Success = false;
            response.Message.Add(" >>> Id do Pedido Inválido <<<");
        }
        if (input.Quantity < 1)
        {
            response.Success = false;
            response.Message.Add(" >>> A Quantidade Minima do Pedido deve ser maior ou igual a UM <<<");
        }
        var productExists = await _productRepository.Get(input.ProductId);
        if (productExists is null)
        {
            response.Success = false;
            response.Message.Add(" >>> O Id do produto é Inválido <<<");
        }
        else if (productExists.Stock < input.Quantity)
        {
            response.Success = false;
            response.Message.Add($" >>> Não há quantidade suficiente no estoque - Disponivel: {productExists.Stock} <<<");
        }
        if (!response.Success)
        {
            return response;
        }
        productExists.Stock = productExists.Stock - input.Quantity;
        await _productRepository.Update(productExists);

        var productOrder = input.ToProductOrder();

        productOrder.UnitPrice = productExists.Price;
        productOrder.SubTotal = productOrder.UnitPrice * productOrder.Quantity;

        order.Total += productOrder.SubTotal;

        await _productOrderRepository.Update(productOrder);
        await _orderRepository.Update(order);
        return new Response<OutputProductOrder> { Success = true, Value = productOrder.ToOuputProductOrder() };
    }

    public async Task<Response<OutputOrder>> Delete(long id)
    {
        var orderExists = await ValidadeteId(id);
        if (!orderExists.Success)
        {
            return new Response<OutputOrder> { Success = false, Message = orderExists.Message };
        }
        _orderRepository.Delete(id);
        return new Response<OutputOrder> { Success = true, Message = { " >>> Pedido Deletado com Sucesso <<<" } };
    }

    //public async Task<Response<Order>> Total()
    //{
    //    var orderExists = await GetAll();
    //    if (!orderExists.Success)
    //    {
    //        return new Response<Order> { Success = false, Message = orderExists.Message };
    //    }
    //    var orderList = orderExists.Value;
    //    (from o in orderList
    //     select o.ProductOrders
    //    }

    public Task<Response<Order>> Total()
    {
        throw new NotImplementedException();
    }

    //public async Task<Response<OutputProduct>> BestSellerProduct()
    //{
    //    var order = await GetAll();
    //    var orderList = order.Value;
    //    var bestSeller = from o in orderList
    //                     select o.ProductOrde



    //}

}
