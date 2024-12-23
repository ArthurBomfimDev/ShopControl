using ProjetoEstagioAPI.Arguments.Order;
using ProjetoEstagioAPI.Infrastructure.UnitOfWork;
using ProjetoEstagioAPI.Mapping.Orders;
using ProjetoEstagioAPI.Models;
using System.Linq;

namespace ProjetoEstagioAPI.Services;

public class OrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response<List<OutputOrder>>> GetAll()
    {
        var orderList = await _unitOfWork.OrderRepository.GetAllAsync();
        if (orderList is null || orderList.Count == 0)
        {
            return new Response<List<OutputOrder>> { Success = false, Message = " >>> Nenhum pedido foi feito <<<" };
        }
        return new Response<List<OutputOrder>> { Success = true, Value = orderList.ToOutputOrderList() };
    }
    public async Task<Response<Order>> Get(long id)
    {
        var order = await _unitOfWork.OrderRepository.Get(id);
        if (order is null)
        {
            return new Response<Order> { Success = false, Message = " >>> Pedido com o Id digitado NÃO encontrada <<<" };
        }
        return new Response<Order> { Success = true, Value = order};
    }
    public async Task<Response<OutputOrder>> Delete(long id)
    {
        var orderExists = await Get(id);
        if (!orderExists.Success)
        {
            return new Response<OutputOrder> { Success = false, Message = orderExists.Message };
        }
        _unitOfWork.OrderRepository.Delete(id);
        return new Response<OutputOrder> { Success = true, Message = " >>> Pedido Deletado com Sucesso <<<" };
    }
    public async Task<Response<OutputOrder>> Create(InputCreateOrder input)
    {
        if (input is null)
        {
            return new Response<OutputOrder> { Success = false, Message = " >>> Dados Inseridos são Inválidos <<<" };
        }
        var clientExists = await _unitOfWork.ClientRepository.Get(input.ClientId);
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
            var productExists = await _unitOfWork.ProductRepository.Get(po.ProductId);
            if (productExists is null)
                return new Response<OutputOrder> { Success = false, Message = " >>> O Id do produto é Inválido <<<" };
            if (productExists.Stock < po.Quantity)
            {
                return new Response<OutputOrder> { Success = false, Message = $" >>> Não há quantidade suficiente no estoque - Disponivel: {productExists.Stock} <<<" };
            }
            productExists.Stock = productExists.Stock - po.Quantity;
            await _unitOfWork.ProductRepository.Update(productExists);
        }
        var createOrder = await _unitOfWork.OrderRepository.Create(input.ToOrder());
        await _unitOfWork.Commit();
        return new Response<OutputOrder> { Success = true, Value = createOrder.ToOutputOrder() };
    }
    public async Task<Response<Order>> Total()
    {
        var orderExists = await GetAll();
        if (!orderExists.Success)
        {
            return new Response<Order> {  Success = false, Message = orderExists.Message };
        }
        var orderList = orderExists.Value;
        (from o in orderList
        select o.ProductOrders
    }
}
