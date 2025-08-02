using ShopControl.Arguments.Arguments.Customer;
using ShopControl.Domain.DTO;
using ShopControl.Domain.DTO.Base;

namespace ShopControl.Arguments.Arguments;

public class CustomerValidateDTO : BaseValidateDTO_1<InputCreateCustomer, InputUpdateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer>
{
    public CustomerDTO? OriginalDTO { get; private set; }
    public CustomerDTO? AlreadyExistsCPF { get; set; }  
    public long? RepeteId { get; private set; }
    public InputIdentityDeleteCustomer RepeatedDelete { get; private set; }

    public CustomerValidateDTO ValidateCreate(InputCreateCustomer inputCreateCustomer, Dictionary<string, List<int>> dictionaryLength, CustomerDTO? alreadyExistsCPF)
    {
        InputCreate = inputCreateCustomer;
        AlreadyExistsCPF = alreadyExistsCPF;
        DictionaryLength = dictionaryLength;
        return this;
    }


    public CustomerValidateDTO ValidateUpdate(InputIdentityUpdateCustomer inputIdentityUpdateCustomer, CustomerDTO original, long repeteId, Dictionary<string, List<int>> dictionaryLength, CustomerDTO? alreadyExistsCPF)
    {
        InputIdentityUpdate = inputIdentityUpdateCustomer;
        OriginalDTO = original;
        AlreadyExistsCPF = alreadyExistsCPF;
        RepeteId = repeteId;
        DictionaryLength = dictionaryLength;
        return this;
    }
    public CustomerValidateDTO ValidateDelete(InputIdentityDeleteCustomer? inputIdentifyDeleteCustomer, CustomerDTO? original, InputIdentityDeleteCustomer repeatedDelete)
    {
        InputIdentityDelete = inputIdentifyDeleteCustomer;
        OriginalDTO = original;
        RepeatedDelete = repeatedDelete;
        return this;
    }
}