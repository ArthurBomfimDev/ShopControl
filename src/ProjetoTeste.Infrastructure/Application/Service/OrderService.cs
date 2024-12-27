using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Interface.Repositories;

namespace ProjetoTeste.Infrastructure.Application.Service;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IClientRepository clientRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
        _productRepository = productRepository;
    }
    public async Task<Response<List<OutputOrder>>> GetAll()
    {
        var orderList = await _orderRepository.GetAllAsync();
        return new Response<List<OutputOrder>> { Success = true, Value = orderList.ToOutputOrderList() };
    }
    public async Task<Response<Order>> Get(long id)
    {
        var order = await _orderRepository.Get(id); 
        return new Response<Order> { Success = true, Value = order };
    }
    public async Task<Response<OutputOrder>> Delete(long id)
    {
        var orderExists = await Get(id);
        if (!orderExists.Success)
        {
            return new Response<OutputOrder> { Success = false, Message = orderExists.Message };
        }
        _orderRepository.Delete(id);
        return new Response<OutputOrder> { Success = true, Message = " >>> Pedido Deletado com Sucesso <<<" };
    }
    public async Task<Response<OutputOrder>> Create(InputCreateOrder input)
    {
        if (input is null)
        {
            return new Response<OutputOrder> { Success = false, Message = " >>> Dados Inseridos são Inválidos <<<" };
        }
        var clientExists = await _clientRepository.Get(input.ClientId);
        if (clientExists is null)
        {
            return new Response<OutputOrder> { Success = false, Message = " >>> Cliente Id Inserido é Inválido <<<" };
        }
        var productOrders = input.ProductOrders;
        foreach (var po in productOrders)
        {
            if (po.Quantity < 1)
            {
                return new Response<OutputOrder> { Success = false, Message = " >>> A Quantidade Minima do Pedido deve ser maior ou igual a UM <<<" };
            }
            var productExists = await _productRepository.Get(po.ProductId);
            if (productExists is null)
                return new Response<OutputOrder> { Success = false, Message = " >>> O Id do produto é Inválido <<<" };
            if (productExists.Stock < po.Quantity)
            {
                return new Response<OutputOrder> { Success = false, Message = $" >>> Não há quantidade suficiente no estoque - Disponivel: {productExists.Stock} <<<" };
            }
            productExists.Stock = productExists.Stock - po.Quantity;
            await _productRepository.Update(productExists);
        }
        var createOrder = await _orderRepository.Create(input.ToOrder());
        return new Response<OutputOrder> { Success = true, Value = createOrder.ToOutputOrder() };
    }
    //    public async Task<Response<Order>> Total()
    //    {
    //        var orderExists = await GetAll();
    //        if (!orderExists.Success)
    //        {
    //            return new Response<Order> { Success = false, Message = orderExists.Message };
    //        }
    //        var orderList = orderExists.Value;
    //        (from o in orderList
    //         select o.ProductOrders
    //    }
}
