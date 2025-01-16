using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Infrastructure.Application.Service.Order;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductOrderRepository _productOrderRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly OrderValidateService _orderValidateService;

    public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository, IProductOrderRepository productOrderRepository, IBrandRepository brandRepository, OrderValidateService orderValidateService)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _productOrderRepository = productOrderRepository;
        _brandRepository = brandRepository;
        _orderValidateService = orderValidateService;
    }

    #region Get
    public async Task<BaseResponse<List<OutputOrder>>> GetAll()
    {
        var listOrder = await _orderRepository.GetProductOrders();
        var outputOrder = listOrder.Select(o => new OutputOrder(o.Id, o.CustomerId, (from i in o.ListProductOrder select i.ToOuputProductOrder()).ToList(), o.Total, o.OrderDate)).ToList();
        return new BaseResponse<List<OutputOrder>> { Success = true, Content = outputOrder };
    }

    public async Task<BaseResponse<List<OutputOrder>>> Get(long id)
    {
        var order = await _orderRepository.GetProductOrdersId(id);
        return new BaseResponse<List<OutputOrder>> { Success = true, Content = order.Select(o => new OutputOrder(o.Id, o.CustomerId, (from i in o.ListProductOrder select i.ToOuputProductOrder()).ToList(), o.Total, o.OrderDate)).ToList() };
    }

    public async Task<BaseResponse<List<OutputOrder>>> GetListByListId(List<long> listId)
    {
        var listOrder = await _orderRepository.GetProductOrdersByListId(listId);
        return new BaseResponse<List<OutputOrder>> { Success = true, Content = listOrder.Select(i => i.ToOutputOrder()).ToList() };
    }
    #endregion

    #region "Relatorio"
    public Task<OutputMaxSaleValueProduct> BestSellerProduct()
    {
        throw new NotImplementedException();
    }

    public Task<OutputMaxSaleValueProduct> LeastSoldProduct()
    {
        throw new NotImplementedException();
    }

    public Task<List<OutputMaxSaleValueProduct>> TopSellers()
    {
        throw new NotImplementedException();
    }

    public Task<OutputCustomerOrder> BiggestBuyer()
    {
        throw new NotImplementedException();
    }

    public Task<OutputCustomerOrder> BiggestBuyerPrice()
    {
        throw new NotImplementedException();
    }

    public Task<OutputMaxSaleValueProduct> OrderBestSeller()
    {
        throw new NotImplementedException();
    }

    public Task<decimal> Avarege()
    {
        throw new NotImplementedException();
    }

    public Task<decimal> Total()
    {
        throw new NotImplementedException();
    }
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

    #region Create Order
    public async Task<BaseResponse<OutputOrder>> Create(InputCreateOrder inputCreateOrder)
    {
        var response = new BaseResponse<OutputOrder>();
        var createValidate = await CreateMultiple([inputCreateOrder]);
        response.Success = createValidate.Success;
        response.Message = createValidate.Message;
        response.Content = createValidate.Content.FirstOrDefault();
        return response;
    }

    public async Task<BaseResponse<List<OutputOrder>>> CreateMultiple(List<InputCreateOrder> listinputCreateOrder)
    {
        var response = new BaseResponse<List<OutputOrder>>();
        var listCustomerId = (await _customerRepository.GetListByListId(listinputCreateOrder.Select(i => i.CustomerId).ToList())).Select(j => j.Id).ToList();
        var listCreate = (from i in listinputCreateOrder
                          select new
                          {
                              InputCreateOrder = i,
                              CustomerId = listCustomerId.FirstOrDefault(j => j == i.CustomerId)
                          });
        List<OrderValidate> listOrderValidate = listCreate.Select(i => new OrderValidate().CreateValidate(i.InputCreateOrder, i.CustomerId)).ToList();
        var create = await _orderValidateService.CreateValidateOrder(listOrderValidate);
        response.Success = create.Success;
        response.Message = create.Message;
        if (!response.Success)
            return response;

        var listCreateOrder = (from i in listOrderValidate
                               select new Order(i.InputCreateOrder.CustomerId, DateTime.Now, default)).ToList();

        var listNewOrder = await _orderRepository.Create(listCreateOrder);
        response.Content = listNewOrder.Select(i => i.ToOutputOrder()).ToList();
        return response;
    }
    #endregion

    #region Create ProductOrder
    public async Task<BaseResponse<OutputProductOrder>> CreateProductOrder(InputCreateProductOrder inputCreateProductOrder)
    {
        var response = new BaseResponse<OutputProductOrder>();
        var createValidate = await CreateProductOrderMultiple([inputCreateProductOrder]);
        response.Success = createValidate.Success;
        response.Message = createValidate.Message;
        response.Content = createValidate.Content.FirstOrDefault();
        return response;
    }

    public async Task<BaseResponse<List<OutputProductOrder>>> CreateProductOrderMultiple(List<InputCreateProductOrder> listinputCreateProductOrder)
    {
        var response = new BaseResponse<List<OutputProductOrder>>();
        var listOrder = await _orderRepository.GetListByListId(listinputCreateProductOrder.Select(i => i.OrderId).ToList());
        var listProduct = await _productRepository.GetListByListId(listinputCreateProductOrder.Select(i => i.ProductId).ToList());
        var listCreate = (from i in listinputCreateProductOrder
                          select new
                          {
                              InputCreateProductOrder = i,
                              OrderId = listOrder.Select(j => j.Id).FirstOrDefault(j => j == i.OrderId),
                              Product = listProduct.FirstOrDefault(k => k.Id == i.ProductId).ToProductDTO(),
                          });
        List<ProductOrderValidate> listProductOrderValidate = listCreate.Select(i => new ProductOrderValidate().CreateValidate(i.InputCreateProductOrder, i.OrderId, i.Product)).ToList();
        var create = await _orderValidateService.CreateValidateProductOrder(listProductOrderValidate);
        response.Success = create.Success;
        response.Message = create.Message;
        if (!response.Success)
            return response;

        var createValidate = (from i in create.Content
                              select new ProductOrder(i.OrderId, i.Product.Id, i.InputCreateProductOrder.Quantity, i.Product.Price, (i.Product.Price * i.InputCreateProductOrder.Quantity))).ToList();
        var listNewProductOrder = await _productOrderRepository.Create(createValidate);

        var listUpdateProduct = await _productRepository.GetListByListId(create.Content.Select(i => i.Product.Id).ToList());
        var listUpdateOrder = await _orderRepository.GetListByListId(create.Content.Select(i => i.OrderId).ToList());
        for (var i = 0; i < listUpdateProduct.Count; i++)
        {
            listUpdateProduct[i].Stock = create.Content[i].Product.Stock;
        }

        var totalOrder = (from i in listUpdateOrder
                          from j in createValidate
                          where i.Id == j.OrderId
                          let total = i.Total += j.SubTotal
                          select i).ToList();

        var updateProduct = await _productRepository.Update(listUpdateProduct);
        var updateOrder = await _orderRepository.Update(totalOrder);
        if (!updateProduct || !updateOrder)
        {
            response.AddErrorMessage("Não foi possivél efetuar o pedido");
            return response;
        }

        response.Content = listNewProductOrder.Select(i => i.ToOuputProductOrder()).ToList();
        return response;
    }
    #endregion

}