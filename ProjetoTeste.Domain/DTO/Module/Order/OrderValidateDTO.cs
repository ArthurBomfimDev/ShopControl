using ProjetoTeste.Arguments.Arguments.Order;

namespace ProjetoTeste.Arguments.Arguments;

public class OrderValidateDTO : BaseValidate
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