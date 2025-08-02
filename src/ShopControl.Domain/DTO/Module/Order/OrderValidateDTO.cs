using ShopControl.Arguments.Arguments.Order;

namespace ShopControl.Arguments.Arguments;

public class OrderValidateDTO : BaseValidateDTO
{
    public InputCreateOrder InputCreateOrder { get; private set; }
    public long CustomerId { get; private set; }

    public OrderValidateDTO()
    {

    }

    public OrderValidateDTO CreateValidate(InputCreateOrder inputCreateOrder, long customerId)
    {
        InputCreateOrder = inputCreateOrder;
        CustomerId = customerId;
        return this;
    }
}