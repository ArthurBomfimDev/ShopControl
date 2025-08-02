using ShopControl.Arguments.Arguments;
using ShopControl.Arguments.Arguments.Base;
using ShopControl.Arguments.Arguments.Base.ApiResponse;
using ShopControl.Arguments.Arguments.Base.Crud;
using ShopControl.Arguments.Arguments.ProductOrder;
using ShopControl.Domain.DTO;
using ShopControl.Domain.Interface.Repository;
using ShopControl.Domain.Interface.Service.Module.ProductOrder;
using ShopControl.Domain.Service.Base;

namespace ShopControl.Domain.Service.Module.ProductOrder;

public class ProductOrderService : BaseService<IProductOrderRepository, IProductOrderValidateService, ProductOrderDTO, InputCreateProductOrder, BaseInputUpdate_0, BaseInputIdentityUpdate_0, BaseInputIdentityDelete_0, BaseInputIdentityView_0, OutputProductOrder, ProductOrderValidateDTO>, IProductOrderService
{
    private readonly IProductOrderRepository _productOrderRepository;
    private readonly IProductOrderValidateService _productOrderValidateService;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public ProductOrderService(IProductOrderRepository repository, IProductOrderValidateService validateService, IOrderRepository orderRepository, IProductRepository productRepository) : base(repository, validateService)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _productOrderValidateService = validateService;
        _productOrderRepository = repository;
    }

    #region Create
    public override async Task<BaseResult<List<OutputProductOrder>>> CreateMultiple(List<InputCreateProductOrder> listinputCreateProductOrder)
    {
        var listOrder = await _orderRepository.GetListByListId(listinputCreateProductOrder.Select(i => i.OrderId).ToList());
        var listProduct = await _productRepository.GetListByListId(listinputCreateProductOrder.Select(i => i.ProductId).ToList());

        var listCreate = (from i in listinputCreateProductOrder
                          select new
                          {
                              InputCreateProductOrder = i,
                              OrderDTO = listOrder.FirstOrDefault(j => j.Id == i.OrderId),
                              Product = listProduct.FirstOrDefault(k => k.Id == i.ProductId)
                          });

        List<ProductOrderValidateDTO> listProductOrderValidate = listCreate.Select(i => new ProductOrderValidateDTO().CreateValidate(i.InputCreateProductOrder, i.OrderDTO, i.Product)).ToList();

        _productOrderValidateService.ValidateCreate(listProductOrderValidate);

        listProductOrderValidate = RemoveInvalid(listProductOrderValidate);

        var listNotification = GetAllNotification();
        if (listNotification!.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<List<OutputProductOrder>>.Failure(listNotification!);

        var createValidate = (from i in RemoveInvalid(listProductOrderValidate)
                              let SubTotal = i.Product.Price * i.InputCreateProductOrder.Quantity
                              let total = i.OrderDTO.Total += SubTotal
                              select new ProductOrderDTO(i.OrderDTO.Id, i.Product.Id, i.InputCreateProductOrder.Quantity, i.Product.Price, SubTotal)).ToList();


        var listNewProductOrder = await _productOrderRepository.Create(createValidate);
        await _productRepository.Update(listProductOrderValidate.Select(i => i.Product).ToList().Distinct().ToList());
        await _orderRepository.Update(listProductOrderValidate.Select(i => i.OrderDTO).ToList().Distinct().ToList());

        return BaseResult<List<OutputProductOrder>>.Success(listNewProductOrder!.Select(i => (OutputProductOrder)i).ToList(), listNotification!);
    }
    #endregion
}