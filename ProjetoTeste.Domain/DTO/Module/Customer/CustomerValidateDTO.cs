using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Domain.DTO;

namespace ProjetoTeste.Arguments.Arguments;

public class CustomerValidateDTO : BaseValidateDTO
{
    public InputCreateCustomer? InputCreateCustomer { get; private set; }
    public InputIdentityUpdateCustomer? InputIdentityUpdateCustomer { get; private set; }
    public InputIdentifyDeleteCustomer? InputIdentifyDeleteCustomer { get; private set; }
    public Domain.DTO.CustomerDTO? OriginalDTO { get; private set; }
    public long? RepeteId { get; private set; }
    public InputIdentifyDeleteCustomer RepeatedDelete { get; private set; }

    public CustomerValidateDTO ValidateCreate(InputCreateCustomer inputCreateCustomer)
    {
        InputCreateCustomer = inputCreateCustomer;
        return this;
    }


    public CustomerValidateDTO ValidateUpdate(InputIdentityUpdateCustomer inputIdentityUpdateCustomer, CustomerDTO original, long repeteId)
    {
        InputIdentityUpdateCustomer = inputIdentityUpdateCustomer;
        OriginalDTO = original;
        RepeteId = repeteId;
        return this;
    }
    public CustomerValidateDTO ValidateDelete(InputIdentifyDeleteCustomer? inputIdentifyDeleteCustomer, CustomerDTO? original, InputIdentifyDeleteCustomer repeatedDelete)
    {
        InputIdentifyDeleteCustomer = inputIdentifyDeleteCustomer;
        OriginalDTO = original;
        RepeatedDelete = repeatedDelete;
        return this;
    }
}