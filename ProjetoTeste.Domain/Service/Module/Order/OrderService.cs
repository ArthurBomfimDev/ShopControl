using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using ProjetoTeste.Arguments.Arguments.Base.Crud;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository;
using ProjetoTeste.Domain.Interface.Service;
using ProjetoTeste.Domain.Service.Base;
using ProjetoTeste.Infrastructure.Interface.ValidateService;

namespace ProjetoTeste.Domain.Service;

public class OrderService : BaseService<IOrderRepository, IOrderValidateService, OrderDTO, InputCreateOrder, BaseInputUpdate_0, BaseInputIdentityUpdate_0, BaseInputIdentityDelete_0, InputIdentifyViewOrder, OutputOrder, OrderValidateDTO>, IOrderService
{
    #region Dependency Injection
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderValidateService _orderValidateService;
    private readonly IBrandRepository _brandRepository;


    public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, IOrderValidateService orderValidateService, IBrandRepository brandRepository) : base(orderRepository, orderValidateService)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _orderValidateService = orderValidateService;
        _brandRepository = brandRepository;
    }
    #endregion

    #region Get
    public override async Task<List<OutputOrder>> GetAll()
    {
        var listOrder = await _orderRepository.GetAllWithProductOrders();
        return listOrder.Select(i => (OutputOrder)i).ToList();
    }

    public async Task<List<OutputOrder>> GetByIdWithProducts(InputIdentifyViewOrder inputIdentifyViewOrder)
    {
        var order = await _orderRepository.GetByIdWithProductOrders(inputIdentifyViewOrder.Id);
        return order.Select(i => (OutputOrder)i).ToList();
    }

    public override async Task<List<OutputOrder>> GetListByListId(List<InputIdentifyViewOrder> listInputIdentifyViewOrder)
    {
        var listOrder = await _orderRepository.GetListByListIdWhithProductOrders(listInputIdentifyViewOrder.Select(i => i.Id).ToList());
        return listOrder.Select(i => (OutputOrder)i).ToList();
    }
    #endregion

    #region "Relatorio"
    public async Task<OutputMaxSaleValueProduct> BestSellerProduct()
    {
        return await _orderRepository.BestSellerProduct();
    }

    public async Task<OutputMaxSaleValueProduct> LeastSoldProduct()
    {
        return await _orderRepository.LeastSoldProduct();
    }

    public async Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct()
    {
        return await _orderRepository.GetMostOrderedProduct();
    }

    public async Task<OutputCustomerOrder> BiggestBuyer()
    {
        var buyer = await _orderRepository.BiggestBuyer();
        var customer = await _customerRepository.Get(buyer.CustomerId);
        return new OutputCustomerOrder(buyer.CustomerId, customer.Name, buyer.TotalOrders, buyer.QuantityPurchased, buyer.TotalPrice);
    }

    public async Task<OutputBrandBestSeller> BrandBestSeller()
    {
        var bestSeller = await _orderRepository.BrandBestSeller();
        var brand = await _brandRepository.Get(bestSeller.Id);
        return new OutputBrandBestSeller(brand.Id, brand.Name, brand.Code, brand.Description, bestSeller.TotalSell, bestSeller.TotalPrice);
    }

    public async Task<HighestAverageSalesValue> HighestAverageSalesValue()
    {
        return await _orderRepository.HighestAverageSalesValue();
    }

    public async Task<string> Total()
    {
        var total = await _orderRepository.Total();
        return $"O Total vendido: R$: {total}";
    }
    #endregion

    #region Create Order
    public override async Task<BaseResult<List<OutputOrder>>> CreateMultiple(List<InputCreateOrder> listinputCreateOrder)
    {
        var response = new BaseResponse<List<OutputOrder>>();

        var listCustomerId = (await _customerRepository.GetListByListId(listinputCreateOrder.Select(i => i.CustomerId).ToList())).Select(j => j.Id).ToList();
        var listCreate = (from i in listinputCreateOrder
                          select new
                          {
                              InputCreateOrder = i,
                              CustomerId = listCustomerId.FirstOrDefault(j => j == i.CustomerId)
                          });

        List<OrderValidateDTO> listOrderValidate = listCreate.Select(i => new OrderValidateDTO().CreateValidate(i.InputCreateOrder, i.CustomerId)).ToList();

        _orderValidateService.ValidateCreate(listOrderValidate);

        var listNotification = GetAllNotification();
        if (listNotification!.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<List<OutputOrder>>.Failure(listNotification!);

        var listCreateOrder = (from i in listOrderValidate
                               select new OrderDTO(i.InputCreateOrder.CustomerId, DateTime.Now, default)).ToList();

        var listNewOrder = await _orderRepository.Create(listCreateOrder);
        return BaseResult<List<OutputOrder>>.Success(listNewOrder!.Select(i => (OutputOrder)i).ToList(), listNotification!);
    }
    #endregion
}