using ProjetoTeste.Arguments.Arguments.Customer;

namespace ProjetoTeste.Arguments.Arguments;

public class CustomerValidate : BaseValidate
{
    public InputCreateCustomer? InputCreateCustomer { get; private set; }
    public InputIdentityUpdateCustomer? InputIdentityUpdateCustomer { get; private set; }
    public InputIdentifyDeleteCustomer? InputIdentifyDeleteCustomer { get; private set; }
    public CustomerDTO? Original { get; private set; }
    public long? RepeteId { get; private set; }
    public InputIdentifyDeleteCustomer RepeatedDelete { get; private set; }

    public CustomerValidate ValidateCreate(InputCreateCustomer inputCreateCustomer)
    {
        InputCreateCustomer = inputCreateCustomer;
        return this;
    }


    public CustomerValidate ValidateUpdate(InputIdentityUpdateCustomer inputIdentityUpdateCustomer, CustomerDTO original, long repeteId)
    {
        InputIdentityUpdateCustomer = inputIdentityUpdateCustomer;
        Original = original;
        RepeteId = repeteId;
        return this;
    }
    public CustomerValidate ValidateDelete(InputIdentifyDeleteCustomer? inputIdentifyDeleteCustomer, CustomerDTO? original, InputIdentifyDeleteCustomer repeatedDelete)
    {
        InputIdentifyDeleteCustomer = inputIdentifyDeleteCustomer;
        Original = original;
        RepeatedDelete = repeatedDelete;
        return this;
    }
}