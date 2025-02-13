using ProjetoTeste.Arguments.Arguments.Order;

namespace ProjetoTeste.Arguments.Arguments;

public class OrderValidate : BaseValidate
{
    public InputCreateOrder InputCreateOrder { get; private set; }
    public long CustomerId { get; private set; }

    public OrderValidate()
    {

    }

    public OrderValidate CreateValidate(InputCreateOrder inputCreateOrder, long customerId)
    {
        InputCreateOrder = inputCreateOrder;
        CustomerId = customerId;
        return this;
    }
}