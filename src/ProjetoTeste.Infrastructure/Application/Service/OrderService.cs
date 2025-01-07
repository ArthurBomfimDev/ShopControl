using ProjetoTeste.Arguments.Arguments.Base;
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

    public async Task<BaseResponse<List<OutputOrder>>> GetAll()
    {
        var orderList = await _orderRepository.GetProductOrders();
        var outputOrder = orderList.Select(o => new OutputOrder(o.Id, o.CustomerId, (from i in o.ListProductOrder select i.ToOuputProductOrder()).ToList(), o.Total, o.OrderDate)).ToList();
        return new BaseResponse<List<OutputOrder>> { Success = true, Content = outputOrder };
    }

    public async Task<BaseResponse<List<OutputOrder>>> Get(long id)
    {
        var order = await _orderRepository.GetProductOrdersId(id);
        return new BaseResponse<List<OutputOrder>> { Success = true, Content = order.Select(o => new OutputOrder(o.Id, o.CustomerId, (from i in o.ListProductOrder select i.ToOuputProductOrder()).ToList(), o.Total, o.OrderDate)).ToList() };
    }

    #region "Relatorio"
    public async Task<BaseResponse<Decimal>> Total()
    {
        var order = await _orderRepository.GetProductOrders();
        if (order.Count() == 0)
            return new BaseResponse<decimal> { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        var total = (from i in order
                     select i.Total).Sum();

        return new BaseResponse<decimal> { Success = true, Content = total };
    }

    public async Task<BaseResponse<List<ProductSell>>> ProductSell()
    {
        var order = await _orderRepository.GetProductOrders();
        if (order.Count() == 0) return new BaseResponse<List<ProductSell>>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        var totalSeller = (from i in order
                           from j in i.ListProductOrder
                           group j by j.ProductId into g
                           select new
                           {
                               productId = g.Key,
                               totalSeller = g.Sum(p => p.Quantity),
                               totalPrice = g.Sum(p => p.SubTotal)
                           }).ToList();
        return new BaseResponse<List<ProductSell>>() { Success = true, Content = totalSeller.Select(p => new ProductSell(p.productId, p.totalSeller, p.totalPrice)).ToList() };
    }

    public async Task<BaseResponse<OutputSellProduct>> BestSellerProduct()
    {
        var totalSeller = await ProductSell();
        if (!totalSeller.Success)
            return new BaseResponse<OutputSellProduct>() { Success = false, Message = totalSeller.Message };
        var BestSeller = totalSeller.Content.MaxBy(x => x.totalSeller);
        var bestSellerProduct = await _productRepository.Get(BestSeller.productId);
        var output = new BaseResponse<OutputSellProduct> { Success = true, Content = new OutputSellProduct(bestSellerProduct.Id, bestSellerProduct.Name, bestSellerProduct.Code, bestSellerProduct.Description, bestSellerProduct.Price, bestSellerProduct.BrandId, bestSellerProduct.Stock, BestSeller.totalSeller) };
        return output;
    }

    public async Task<BaseResponse<List<OutputSellProduct>>> TopSellers()
    {
        var totalSeller = await ProductSell();
        if (!totalSeller.Success)
        {
            return new BaseResponse<List<OutputSellProduct>>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        }
        var top = totalSeller.Content.OrderByDescending(t => t.totalSeller).Take(5);
        var list = new List<OutputSellProduct>();
        foreach (var item in top)
        {
            var bestSellerProduct = await _productRepository.Get(item.productId);
            list.Add(new OutputSellProduct(bestSellerProduct.Id, bestSellerProduct.Name, bestSellerProduct.Code, bestSellerProduct.Description, bestSellerProduct.Price, bestSellerProduct.BrandId, bestSellerProduct.Stock, item.totalSeller));
        }
        list.OrderBy(p => p.QuantitySold);
        return new BaseResponse<List<OutputSellProduct>>() { Success = true, Content = list };
    }

    public async Task<BaseResponse<OutputSellProduct>> LeastSoldProduct()
    {
        var totalSeller = await ProductSell();
        if (!totalSeller.Success)
        {
            return new BaseResponse<OutputSellProduct>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        }
        var BestSeller = totalSeller.Content.MinBy(x => x.totalSeller);
        var bestSellerProduct = await _productRepository.Get(BestSeller.productId);
        var output = new OutputSellProduct(bestSellerProduct.Id, bestSellerProduct.Name, bestSellerProduct.Code, bestSellerProduct.Description, bestSellerProduct.Price, bestSellerProduct.BrandId, bestSellerProduct.Stock, BestSeller.totalSeller);
        return new BaseResponse<OutputSellProduct>() { Success = true, Content = output };
    }

    public async Task<BaseResponse<List<Buy>>> ClientOrder()
    {
        var order = await _orderRepository.GetProductOrdersLINQ();
        if (order.Count() == 0)
            return new BaseResponse<List<Buy>>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
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
        return new BaseResponse<List<Buy>>() { Success = true, Content = buyer.Select(b => new Buy(b.ClientId, b.Orders, b.TotalBuyer, b.TotalPrice)).ToList() };
    }

    public async Task<BaseResponse<OutputCustomerOrder>> BiggestBuyer()
    {
        var order = await ClientOrder();
        if (!order.Success)
            return new BaseResponse<OutputCustomerOrder>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        var buyer = order.Content.MaxBy(b => b.TotalBuyer);
        var client = await _clientRepository.Get(buyer.ClientId);
        return new BaseResponse<OutputCustomerOrder>() { Success = true, Content = new OutputCustomerOrder(buyer.ClientId, client.Name, buyer.Orders, buyer.TotalBuyer, buyer.TotalPrice) };
    }

    public async Task<BaseResponse<OutputCustomerOrder>> BiggestBuyerPrice()
    {
        var order = await ClientOrder();
        if (!order.Success)
            return new BaseResponse<OutputCustomerOrder>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        var buyer = order.Content.MaxBy(b => b.TotalPrice);
        var client = await _clientRepository.Get(buyer.ClientId);
        return new BaseResponse<OutputCustomerOrder>() { Success = true, Content = new OutputCustomerOrder(buyer.ClientId, client.Name, buyer.Orders, buyer.TotalBuyer, buyer.TotalPrice) };
    }

    public async Task<BaseResponse<OutputBrandBestSeller>> BrandBestSeller()
    {
        var order = await _orderRepository.GetProductOrdersLINQ();
        if (order.Count() == 0)
            return new BaseResponse<OutputBrandBestSeller>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
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
        return new BaseResponse<OutputBrandBestSeller>() { Success = true, Content = new OutputBrandBestSeller(brand.Id, brand.Name, brand.Code, brand.Description, brandBestSeller.TotalSell, brandBestSeller.TotalPrice) };
    }

    public async Task<BaseResponse<decimal>> Avarege()
    {
        var order = await _orderRepository.GetProductOrders();
        if (order.Count() == 0)
            return new BaseResponse<decimal>() { Success = false, Message = { " >>> Lista De Pedidos Vazia <<<" } };
        var avarage = (from i in order
                       from j in i.ListProductOrder
                       group j by j.OrderId into g
                       select new
                       {
                           OrderId = g.Key,
                           avaragePrice = g.Average(o => o.SubTotal),
                       }).MaxBy(o => o.avaragePrice);
        return new BaseResponse<decimal>() { Success = true, Content = avarage.avaragePrice };
    }
    #endregion 

    // Seperar em validate para update, create e delete
    public async Task<BaseResponse<bool>> ValidateId(long id)
    {
        var orderExists = await Get(id);
        if (orderExists is null)
        {
            return new BaseResponse<bool> { Success = false, Message = { " >>> Pedido com o Id digitado NÃO encontrado <<<" } };
        }
        return new BaseResponse<bool> { Success = true };
    }

    public async Task<BaseResponse<OutputOrder>> Create(InputCreateOrder input)
    {
        var clientExists = await _clientRepository.Get(input.CustomerId);
        if (clientExists is null)
        {
            return new BaseResponse<OutputOrder> { Success = false, Message = { " >>> Cliente com o Id digitado NÃO encontrado <<<" } };
        }
        var createOrder = await _orderRepository.Create(input.ToOrder());
        return new BaseResponse<OutputOrder> { Success = true, Content = createOrder.ToOutputOrder() };
    }

    public async Task<BaseResponse<OutputProductOrder>> CreateProductOrder(InputCreateProductOrder input)
    {
        var response = new BaseResponse<OutputProductOrder>();
        if (input is null)
        {
            return new BaseResponse<OutputProductOrder>() { Success = false, Message = { " >>> Dados Inseridos são Inválidos <<<" } };
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
        var product = await _productRepository.Get(input.ProductId);
        if (product is null)
        {
            response.Success = false;
            response.Message.Add(" >>> O Id do produto é Inválido <<<");
            return response;
        }
        if (product.Stock < input.Quantity)
        {
            response.Success = false;
            response.Message.Add($" >>> Não há quantidade suficiente no estoque - Disponivel: {product.Stock} <<<");
        }
        if (!response.Success)
        {
            return response;
        }
        product.Stock = product.Stock - input.Quantity;
        await _productRepository.Update(product);

        var subTotal = product.Price * input.Quantity;
        var productOrder = new ProductOrder(input.OrderId, input.ProductId, input.Quantity, product.Price, subTotal);

        order.Total += productOrder.SubTotal;

        await _productOrderRepository.Update(productOrder);
        await _orderRepository.Update(order);

        return new BaseResponse<OutputProductOrder> { Success = true, Content = productOrder.ToOuputProductOrder() };
    }

    // Alterar valor total e estoque
    public async Task<BaseResponse<OutputOrder>> Delete(long id)
    {
        var orderExists = await ValidateId(id);
        if (!orderExists.Success)
        {
            return new BaseResponse<OutputOrder> { Success = false, Message = orderExists.Message };
        }
        _orderRepository.Delete(id);
        return new BaseResponse<OutputOrder> { Success = true, Message = { " >>> Pedido Deletado com Sucesso <<<" } };
    }

}