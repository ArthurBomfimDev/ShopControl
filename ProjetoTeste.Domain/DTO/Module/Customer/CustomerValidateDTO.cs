using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.DTO.Base;

namespace ProjetoTeste.Arguments.Arguments;

public class CustomerValidateDTO : BaseValidateDTO_1<InputCreateCustomer, InputUpdateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer>
{
    public CustomerDTO? OriginalDTO { get; private set; }
    public long? RepeteId { get; private set; }
    public InputIdentityDeleteCustomer RepeatedDelete { get; private set; }

    public CustomerValidateDTO ValidateCreate(InputCreateCustomer inputCreateCustomer, Dictionary<string, List<int>> dictionaryLength)
    {
        InputCreate = inputCreateCustomer;
        DictionaryLength = dictionaryLength;
        return this;
    }


    public CustomerValidateDTO ValidateUpdate(InputIdentityUpdateCustomer inputIdentityUpdateCustomer, CustomerDTO original, long repeteId, Dictionary<string, List<int>> dictionaryLength)
    {
        InputIdentityUpdate = inputIdentityUpdateCustomer;
        OriginalDTO = original;
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