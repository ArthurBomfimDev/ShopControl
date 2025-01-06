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
    private readonly ICustomerRepository _clientRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductOrderRepository _productOrderRepository;
    private readonly IBrandRepository _brandRepository;

    public OrderService(IOrderRepository orderRepository, ICustomerRepository clientRepository, IProductRepository productRepository, IProductOrderRepository productOrderRepository, IBrandRepository brandRepository)
    {
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
        _productRepository = productRepository;
        _productOrderRepository = productOrderRepository;
        _brandRepository = brandRepository;
    }

    public async Task<Response<List<OutputOrder>>> GetAll()
    {
        var orderList = await _orderRepository.GetProductOrders();
        var outputOrder = orderList.Select(o => new OutputOrder(o.Id, o.CustomerId, (from i in o.ListProductOrder select i.ToOuputProductOrder()).ToList(), o.Total, o.OrderDate)).ToList();
        return new Response<List<OutputOrder>> { Success = true, Value = outputOrder };
    }

    public async Task<Response<List<OutputOrder>>> Get(long id)
    {
        var order = await _orderRepository.GetProductOrdersId(id);
        return new Response<List<OutputOrder>> { Success = true, Value = order.Select(o => new OutputOrder(o.Id, o.CustomerId, (from i in o.ListProductOrder select i.ToOuputProductOrder()).ToList(), o.Total, o.OrderDate)).ToList() };
    }

    #region "LINQ"
    public async Task<Decimal> Total()
    {
        var order = await _orderRepository.GetProductOrders();
        var total = (from i in order
                     select i.Total).Sum();

        return total;
    }

    public async Task<List<ProductSell>> ProductSell()
    {
        var order = await _orderRepository.GetProductOrders();
        var totalSeller = (from i in order
                           from j in i.ListProductOrder
                           group j by j.ProductId into g
                           select new
                           {
                               productId = g.Key,
                               totalSeller = g.Sum(p => p.Quantity),
                               totalPrice = g.Sum(p => p.SubTotal)
                           }).ToList();
        return totalSeller.Select(p => new ProductSell(p.productId, p.totalSeller, p.totalPrice)).ToList();
    }

    public async Task<Response<OutputSellProduct>> BestSellerProduct()
    {
        var totalSeller = await ProductSell();
        if(totalSeller is null)
        {
            return new Response<OutputSellProduct>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        }
        var BestSeller = totalSeller.MaxBy(x => x.totalSeller);
        var bestSellerProduct = await _productRepository.Get(BestSeller.productId);
        var output = new Response<OutputSellProduct> { Success = true, Value = new OutputSellProduct(bestSellerProduct.Id, bestSellerProduct.Name, bestSellerProduct.Code, bestSellerProduct.Description, bestSellerProduct.Price, bestSellerProduct.BrandId, bestSellerProduct.Stock, BestSeller.totalSeller) };
        return output;
    }

    public async Task<Response<List<OutputSellProduct>>> TopSellers()
    {
        var totalSeller = await ProductSell();
        if (totalSeller is null)
        {
            return new Response<List<OutputSellProduct>>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        }
        var top = totalSeller.OrderByDescending(t => t.totalSeller).Take(5);
        var list = new List<OutputSellProduct>();
        foreach (var item in top)
        {
            var bestSellerProduct = await _productRepository.Get(item.productId);
            list.Add(new OutputSellProduct(bestSellerProduct.Id, bestSellerProduct.Name, bestSellerProduct.Code, bestSellerProduct.Description, bestSellerProduct.Price, bestSellerProduct.BrandId, bestSellerProduct.Stock, item.totalSeller));
        }
        list.OrderBy(p => p.QuantitySold);
        return new Response<List<OutputSellProduct>>() { Success = false, Value = list };
    }

    public async Task<Response<OutputSellProduct>> LesatSoldProduct()
    {
        var totalSeller = await ProductSell();
        if (totalSeller is null)
        {
            return new Response<OutputSellProduct>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        }
        var BestSeller = totalSeller.MinBy(x => x.totalSeller);
        var bestSellerProduct = await _productRepository.Get(BestSeller.productId);
        var output = new OutputSellProduct(bestSellerProduct.Id, bestSellerProduct.Name, bestSellerProduct.Code, bestSellerProduct.Description, bestSellerProduct.Price, bestSellerProduct.BrandId, bestSellerProduct.Stock, BestSeller.totalSeller);
        return new Response<OutputSellProduct>() { Success = true, Value = output };
    }

    public async Task<List<Buy>> ClientOrder()
    {
        var order = await _orderRepository.GetProductOrdersLINQ();
        var buyer = (from i in order
                     from j in i.ListProductOrder
                     group j by i.CustomerId into g
                     select new
                     {
                         ClientId = g.Key,
                         Orders = g.Select(p => p.OrderId),
                         TotalBuyer = g.Sum(p => p.Quantity),
                         TotalPrice = g.Sum(p => p.SubTotal)
                     }).ToList();
        return buyer.Select(b => new Buy(b.ClientId, b.Orders, b.TotalBuyer, b.TotalPrice)).ToList();
    }

    public async Task<OutputCustomerOrder> BiggestBuyer()
    {
        var order = await ClientOrder();
        var buyer = order.MaxBy(b => b.TotalBuyer);
        var client = await _clientRepository.Get(buyer.ClientId);
        return new OutputCustomerOrder(buyer.ClientId, client.Name, buyer.Orders, buyer.TotalBuyer, buyer.TotalPrice);
    }

    public async Task<OutputCustomerOrder> BiggestBuyerPrice()
    {
        var order = await ClientOrder();
        var buyer = order.MaxBy(b => b.TotalPrice);
        var client = await _clientRepository.Get(buyer.ClientId);
        return new OutputCustomerOrder(buyer.ClientId, client.Name, buyer.Orders, buyer.TotalBuyer, buyer.TotalPrice);
    }

    public async Task<OutputBrandBestSeller> BrandBestSeller()
    {
        var order = await _orderRepository.GetProductOrdersLINQ();
        var brandShere = (from i in order
                          from j in i.ListProductOrder
                          group j by j.ProductId into g
                          select new
                          {
                              productId = g.Key,
                              totalSeller = g.Sum(p => p.Quantity),
                              totalPrice = g.Sum(p => p.SubTotal),
                              brandId = _productRepository.BrandId(g.Key)
                          }).ToList();
        var brandBestSeller = (from i in brandShere
                               group i by i.brandId into g
                               select new
                               {
                                   brandId = g.Key,
                                   TotalSell = g.Sum(b => b.totalSeller),
                                   TotalPrice = g.Sum(b => b.totalPrice),
                               }).MaxBy(b => b.TotalSell);
        var brand = await _brandRepository.Get(brandBestSeller.brandId);
        return new OutputBrandBestSeller(brand.Id, brand.Name, brand.Code, brand.Description, brandBestSeller.TotalSell, brandBestSeller.TotalPrice);
    }

    public async Task<decimal> Avarege()
    {
        var order = await _orderRepository.GetProductOrders();
        var avarage = (from i in order
                       from j in i.ListProductOrder
                       group j by j.OrderId into g
                       select new
                       {
                           OrderId = g.Key,
                           avaragePrice = g.Average(o => o.SubTotal),
                       }).MaxBy(o => o.avaragePrice);
        return avarage.avaragePrice;
    }
    #endregion 

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
        var clientExists = await _clientRepository.Get(input.CustomerId);
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

}