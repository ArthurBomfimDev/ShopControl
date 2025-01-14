using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Application;

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
    //public async task<baseresponse<decimal>> total()
    //{
    //    var order = await _orderrepository.getproductorders();
    //    var response = new baseresponse<decimal>();
    //    if (order.count() == 0)
    //    {
    //        response.success = false;
    //        response.adderrormessage(" >>> lista de pedidos vazia <<<");
    //    }

    //    var total = (from i in order
    //                 select i.total).sum();

    //    response.content = total;
    //    return response;
    //}

    //public async task<baseresponse<list<productsell>>> productsell()
    //{
    //    var order = await _orderrepository.getproductorders();
    //    var totalseller = (from i in order
    //                       from j in i.listproductorder
    //                       group j by j.productid into g
    //                       select new
    //                       {
    //                           productid = g.key,
    //                           totalseller = g.sum(p => p.quantity),
    //                           totalprice = g.sum(p => p.subtotal)
    //                       }).tolist();
    //    return new baseresponse<list<productsell>>() { success = true, content = totalseller.select(p => new productsell(p.productid, p.totalseller, p.totalprice)).tolist() };
    //}

    //public async task<baseresponse<outputsellproduct>> bestsellerproduct()
    //{
    //    var totalseller = await productsell();
    //    if (!totalseller.success)
    //        return new baseresponse<outputsellproduct>() { success = false, message = totalseller.message };
    //    var bestseller = totalseller.content.maxby(x => x.totalseller);
    //    var bestsellerproduct = await _productrepository.get(bestseller.productid);
    //    var output = new baseresponse<outputsellproduct> { success = true, content = new outputsellproduct(bestsellerproduct.id, bestsellerproduct.name, bestsellerproduct.code, bestsellerproduct.description, bestsellerproduct.price, bestsellerproduct.brandid, bestsellerproduct.stock, bestseller.totalseller) };
    //    return output;
    //}

    //public async task<baseresponse<list<outputsellproduct>>> topsellers()
    //{
    //    var totalseller = await productsell();
    //    if (!totalseller.success)
    //    {
    //        return new baseresponse<list<outputsellproduct>>() { success = false, message = { " >>> lista de pedidos vazia <<<" } };
    //    }
    //    var top = totalseller.content.orderbydescending(t => t.totalseller).take(5);
    //    var list = new list<outputsellproduct>();
    //    foreach (var item in top)
    //    {
    //        var bestsellerproduct = await _productrepository.get(item.productid);
    //        list.add(new outputsellproduct(bestsellerproduct.id, bestsellerproduct.name, bestsellerproduct.code, bestsellerproduct.description, bestsellerproduct.price, bestsellerproduct.brandid, bestsellerproduct.stock, item.totalseller));
    //    }
    //    list.orderby(p => p.quantitysold);
    //    return new baseresponse<list<outputsellproduct>>() { success = true, content = list };
    //}

    //public async task<baseresponse<outputsellproduct>> leastsoldproduct()
    //{
    //    var totalseller = await productsell();
    //    if (!totalseller.success)
    //    {
    //        return new baseresponse<outputsellproduct>() { success = false, message = { " >>> lista de pedidos vazia <<<" } };
    //    }
    //    var bestseller = totalseller.content.minby(x => x.totalseller);
    //    var bestsellerproduct = await _productrepository.get(bestseller.productid);
    //    var output = new outputsellproduct(bestsellerproduct.id, bestsellerproduct.name, bestsellerproduct.code, bestsellerproduct.description, bestsellerproduct.price, bestsellerproduct.brandid, bestsellerproduct.stock, bestseller.totalseller);
    //    return new baseresponse<outputsellproduct>() { success = true, content = output };
    //}

    //public async task<baseresponse<list<buy>>> clientorder()
    //{
    //    var order = await _orderrepository.getproductorderslinq();
    //    if (order.count() == 0)
    //        return new baseresponse<list<buy>>() { success = false, message = { " >>> lista de pedidos vazia <<<" } };
    //    var buyer = (from i in order
    //                 from j in i.listproductorder
    //                 group j by i.customerid into g
    //                 select new
    //                 {
    //                     clientid = g.key,
    //                     orders = g.select(p => p.orderid),
    //                     totalbuyer = g.sum(p => p.quantity),
    //                     totalprice = g.sum(p => p.subtotal)
    //                 }).tolist();
    //    return new baseresponse<list<buy>>() { success = true, content = buyer.select(b => new buy(b.clientid, b.orders, b.totalbuyer, b.totalprice)).tolist() };
    //}

    //public async task<baseresponse<outputcustomerorder>> biggestbuyer()
    //{
    //    var order = await clientorder();
    //    if (!order.success)
    //        return new baseresponse<outputcustomerorder>() { success = false, message = { " >>> lista de pedidos vazia <<<" } };
    //    var buyer = order.content.maxby(b => b.totalbuyer);
    //    var client = await _clientrepository.get(buyer.clientid);
    //    return new baseresponse<outputcustomerorder>() { success = true, content = new outputcustomerorder(buyer.clientid, client.name, buyer.orders, buyer.totalbuyer, buyer.totalprice) };
    //}

    //public async task<baseresponse<outputcustomerorder>> biggestbuyerprice()
    //{
    //    var order = await clientorder();
    //    if (!order.success)
    //        return new baseresponse<outputcustomerorder>() { success = false, message = { " >>> lista de pedidos vazia <<<" } };
    //    var buyer = order.content.maxby(b => b.totalprice);
    //    var client = await _clientrepository.get(buyer.clientid);
    //    return new baseresponse<outputcustomerorder>() { success = true, content = new outputcustomerorder(buyer.clientid, client.name, buyer.orders, buyer.totalbuyer, buyer.totalprice) };
    //}

    //public async task<baseresponse<outputbrandbestseller>> brandbestseller()
    //{
    //    var order = await _orderrepository.getproductorderslinq();
    //    if (order.count() == 0)
    //        return new baseresponse<outputbrandbestseller>() { success = false, message = { " >>> lista de pedidos vazia <<<" } };
    //    var brandshere = (from i in order
    //                      from j in i.listproductorder
    //                      group j by j.productid into g
    //                      select new
    //                      {
    //                          productid = g.key,
    //                          totalseller = g.sum(p => p.quantity),
    //                          totalprice = g.sum(p => p.subtotal),
    //                          brandid = _productrepository.brandid(g.key)
    //                      }).tolist();
    //    var brandbestseller = (from i in brandshere
    //                           group i by i.brandid into g
    //                           select new
    //                           {
    //                               brandid = g.key,
    //                               totalsell = g.sum(b => b.totalseller),
    //                               totalprice = g.sum(b => b.totalprice),
    //                           }).maxby(b => b.totalsell);
    //    var brand = await _brandrepository.get(brandbestseller.brandid);
    //    return new baseresponse<outputbrandbestseller>() { success = true, content = new outputbrandbestseller(brand.id, brand.name, brand.code, brand.description, brandbestseller.totalsell, brandbestseller.totalprice) };
    //}

    //public async task<baseresponse<decimal>> avarege()
    //{
    //    var order = await _orderrepository.getproductorders();
    //    if (order.count() == 0)
    //        return new baseresponse<decimal>() { success = false, message = { " >>> lista de pedidos vazia <<<" } };
    //    var avarage = (from i in order
    //                   from j in i.listproductorder
    //                   group j by j.orderid into g
    //                   select new
    //                   {
    //                       orderid = g.key,
    //                       avarageprice = g.average(o => o.subtotal),
    //                   }).maxby(o => o.avarageprice);
    //    return new baseresponse<decimal>() { success = true, content = avarage.avarageprice };
    //}
    #endregion

    //seperar em validate para update, create e delete
    //public async task<baseresponse<bool>> validateid(long id)
    //{
    //    var orderexists = await get(id);
    //    if (orderexists is null)
    //    {
    //        return new baseresponse<bool> { success = false, message = { " >>> pedido com o id digitado não encontrado <<<" } };
    //    }
    //    return new baseresponse<bool> { success = true };
    //}

    //public async task<baseresponse<outputorder>> create(inputcreateorder input)
    //{
    //    var clientexists = await _clientrepository.get(input.customerid);
    //    if (clientexists is null)
    //    {
    //        return new baseresponse<outputorder> { success = false, message = { " >>> cliente com o id digitado não encontrado <<<" } };
    //    }
    //    var createorder = await _orderrepository.create(input.toorder());
    //    return new baseresponse<outputorder> { success = true, content = createorder.tooutputorder() };
    //}

    //public async task<baseresponse<outputproductorder>> createproductorder(inputcreateproductorder input)
    //{
    //    var response = new baseresponse<outputproductorder>();
    //    if (input is null)
    //    {
    //        return new baseresponse<outputproductorder>() { success = false, message = { " >>> dados inseridos são inválidos <<<" } };
    //    }
    //    var order = await _orderrepository.get(input.orderid);
    //    if (order is null)
    //    {
    //        response.success = false;
    //        response.message.add(" >>> id do pedido inválido <<<");
    //    }
    //    if (input.quantity < 1)
    //    {
    //        response.success = false;
    //        response.message.add(" >>> a quantidade minima do pedido deve ser maior ou igual a um <<<");
    //    }
    //    var product = await _productrepository.get(input.productid);
    //    if (product is null)
    //    {
    //        response.success = false;
    //        response.message.add(" >>> o id do produto é inválido <<<");
    //        return response;
    //    }
    //    if (product.stock < input.quantity)
    //    {
    //        response.success = false;
    //        response.message.add($" >>> não há quantidade suficiente no estoque - disponivel: {product.stock} <<<");
    //    }
    //    if (!response.success)
    //    {
    //        return response;
    //    }
    //    product.stock = product.stock - input.quantity;
    //    await _productrepository.update(product);

    //    var subtotal = product.price * input.quantity;
    //    var productorder = new productorder(input.orderid, input.productid, input.quantity, product.price, subtotal);

    //    order.total += productorder.subtotal;

    //    await _productorderrepository.update(productorder);
    //    await _orderrepository.update(order);

    //    return new baseresponse<outputproductorder> { success = true, content = productorder.toouputproductorder() };
    //}

    // Alterar valor total e estoque
    public async Task<BaseResponse<OutputOrder>> Delete(List<long> ids)
    {
        //var orderExists = await ValidateId(ids);
        //if (!orderExists.Success)
        //{
        //    return new BaseResponse<OutputOrder> { Success = false, Message = orderExists.Message };
        //}
        //_orderRepository.Delete(id);
        throw new NotImplementedException();

    }

    public Task<BaseResponse<OutputOrder>> Create(InputCreateOrder input)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<OutputProductOrder>> CreateProductOrder(InputCreateProductOrder input)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<OutputMaxSaleValueProduct>> BestSellerProduct()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<OutputMaxSaleValueProduct>> LeastSoldProduct()
    {
        throw new NotImplementedException();
    }

    public async Task<List<OutputMaxSaleValueProduct>> TopSellers()
    {
        var TopSellers = await _orderRepository.GetMostOrderedProduct();
        return TopSellers;
    }

    public Task<BaseResponse<OutputCustomerOrder>> BiggestBuyer()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<OutputCustomerOrder>> BiggestBuyerPrice()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<OutputBrandBestSeller>> BrandBestSeller()
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<decimal>> Avarege()
    {
        throw new NotImplementedException();
    }

    Task<OutputMaxSaleValueProduct> IOrderService.BestSellerProduct()
    {
        throw new NotImplementedException();
    }

    Task<OutputMaxSaleValueProduct> IOrderService.LeastSoldProduct()
    {
        throw new NotImplementedException();
    }

    Task<OutputCustomerOrder> IOrderService.BiggestBuyer()
    {
        throw new NotImplementedException();
    }

    Task<OutputCustomerOrder> IOrderService.BiggestBuyerPrice()
    {
        throw new NotImplementedException();
    }
    Task<decimal> IOrderService.Avarege()
    {
        throw new NotImplementedException();
    }

    Task<decimal> IOrderService.Total()
    {
        throw new NotImplementedException();
    }

    public Task<List<OutputOrder>> GetListByListId(List<long> idList)
    {
        throw new NotImplementedException();
    }

    Task<BaseResponse<List<OutputOrder>>> IOrderService.Create(InputCreateOrder inputCreateOrder)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<List<OutputOrder>>> CreateMultiple(List<InputCreateOrder> inputCreateOrder)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<List<OutputProductOrder>>> CreateProductOrder(List<InputCreateProductOrder> inputCreateProductOrder)
    {
        throw new NotImplementedException();
    }

    public Task<OutputMaxSaleValueProduct> OrderBestSeller()
    {
        throw new NotImplementedException();
    }
}