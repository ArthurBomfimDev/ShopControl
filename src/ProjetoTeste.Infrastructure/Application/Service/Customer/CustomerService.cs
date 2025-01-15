using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerValidateService _customerValidateService;

    public CustomerService(ICustomerRepository customerRepository, CustomerValidateService customerValidateService)
    {
        _customerRepository = customerRepository;
        _customerValidateService = customerValidateService;
    }

    #region Get
    public async Task<List<OutputCustomer>> GetAll()
    {
        var customerList = await _customerRepository.GetAll();
        return (from i in customerList select i.ToOutputCustomer()).ToList();
    }

    public async Task<OutputCustomer> Get(long id)
    {
        var customer = await _customerRepository.Get(id);
        return customer.ToOutputCustomer();
    }

    public async Task<List<OutputCustomer>> GetListByListId(List<long> listId)
    {
        var customerList = await _customerRepository.GetListByListId(idList);
        return (from i in customerList select i.ToOutputCustomer()).ToList();
    }
    #endregion

    #region Create
    public async Task<BaseResponse<OutputCustomer>> Create(InputCreateCustomer inputCreateCustomer)
    {
        var response = new BaseResponse<OutputCustomer>();
        var validateCreate = await CreateMultiple([inputCreateCustomer]);
        response.Success = validateCreate.Success;
        response.Message = validateCreate.Message;
        response.Content = validateCreate.Content.FirstOrDefault();
        return response;
    }

    public async Task<BaseResponse<List<OutputCustomer>>> CreateMultiple(List<InputCreateCustomer> listInputCreateCustomer)
    {
        var response = new BaseResponse<List<OutputCustomer>>();
        List<CustomerValidate> listCutomerValidate = listInputCreateCustomer.Select(i => new CustomerValidate().ValidateCreate(i)).ToList();
        var validateCreate = await _customerValidateService.ValidateCreate(listCutomerValidate);

        response.Success = validateCreate.Success;
        response.Message = validateCreate.Message;
        if (!response.Success) return response;

        var listCreate = ((validateCreate.Content).Select(i => i.InputCreateCustomer).ToList()).Select(i => new Customer(i.Name, i.CPF, i.Email, i.Phone)).ToList();
        var create = await _customerRepository.Create(listCreate);
        response.Content = create.Select(i => i.ToOutputCustomer()).ToList();
        return response;
    }
    #endregion

    #region Update
    public async Task<BaseResponse<bool>> Update(InputIdentityUpdateCustomer inputIdentityUpdateCustomer)
    {
        return await UpdateMultiple([inputIdentityUpdateCustomer]);
    }

    public async Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer)
    {
        var response = new BaseResponse<bool>();
        var originalCustomer = await _customerRepository.GetListByListId(listInputIdentityUpdateCustomer.Select(i => i.Id).ToList())
        var listRepeteId = (from i in listInputIdentityUpdateCustomer
                            where listInputIdentityUpdateCustomer.Count(j => j.Id == i.Id) > 1
                            select i.Id).ToList();

        var listUpdate = (from i in listInputIdentityUpdateCustomer
                          select new
                          {
                              InputIdentityUpdateCustomer = i,
                              OriginalCustomer = originalCustomer.FirstOrDefault(j => j.Id == i.Id).ToCustomerDTO(),
                              RepeteId = listRepeteId.FirstOrDefault(k => k == i.Id),
                          }).ToList();
        
        List<CustomerValidate> customerValidate = listUpdate.Select(i => new CustomerValidate().ValidateUpdate(i.InputIdentityUpdateCustomer, i.OriginalCustomer, i.RepeteId)).ToList();
        var updateValidate = await _customerValidateService.ValidateUpdate(customerValidate);
        response.Success = updateValidate.Success;
        response.Message = updateValidate.Message;
        if (!response.Success)
        {
            response.Content = false;
            return response;
        }

        return response;
    }
    #endregion

    #region Delete
    public Task<BaseResponse<bool>> Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<bool>> DeleteMultiple(List<long> listId)
    {
        var validateDelete = await _validateDeleteRepository.ValidateDelete(listId);
        var response = new BaseResponse<bool>() { Message = validateDelete.Message };

        if (!validateDelete.Success)
        {
            response.Success = false;
            return response;
        }

        await _customerRepository.Delete(validateDelete.Content);
        foreach (var customer in validateDelete.Content)
        {
            response.AddSuccessMessage($" >>> Cliente: {customer.Name} com Id: {customer.Id} foi deletado com SUCESSO <<<");
        }
        return response;
    }
    #endregion

}