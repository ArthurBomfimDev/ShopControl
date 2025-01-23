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
    #region Dependency Injection
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerValidateService _customerValidateService;

    public CustomerService(ICustomerRepository customerRepository, CustomerValidateService customerValidateService)
    {
        _customerRepository = customerRepository;
        _customerValidateService = customerValidateService;
    }
    #endregion

    #region Get
    public async Task<List<OutputCustomer>> GetAll()
    {
        var customerList = await _customerRepository.GetAll();
        return (from i in customerList select i.ToOutputCustomer()).ToList();
    }

    public async Task<OutputCustomer> Get(InputIdentifyViewCustomer inputIdentifyViewCustomer)
    {
        var customer = await _customerRepository.Get(inputIdentifyViewCustomer.Id);
        return customer.ToOutputCustomer();
    }

    public async Task<List<OutputCustomer>> GetListByListId(List<InputIdentifyViewCustomer> listInputIdentifyViewCustomer)
    {
        var customerList = await _customerRepository.GetListByListId(listInputIdentifyViewCustomer.Select(i => i.Id).ToList());
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
        if (!response.Success)
            return response;
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
        var originalCustomer = await _customerRepository.GetListByListId(listInputIdentityUpdateCustomer.Select(i => i.Id).ToList());
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

        var listUpdateCustomer = (from i in updateValidate.Content
                                  from j in originalCustomer
                                  where i.InputIdentityUpdateCustomer.Id == j.Id
                                  let name = j.Name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
                                  let cpf = j.CPF = i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF
                                  let email = j.Email = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email
                                  let phone = j.Phone = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone
                                  let message = response.AddSuccessMessage($"O cliente com o ID: '{i.InputIdentityUpdateCustomer.Id}' foi atualizado com sucesso.")
                                  select j).ToList();

        response.Content = await _customerRepository.Update(listUpdateCustomer);
        return response;
    }
    #endregion

    #region Delete
    public async Task<BaseResponse<bool>> Delete(InputIdentifyDeleteCustomer inputIdentifyDeleteCustomer)
    {
        return await DeleteMultiple([inputIdentifyDeleteCustomer]);
    }

    public async Task<BaseResponse<bool>> DeleteMultiple(List<InputIdentifyDeleteCustomer> listInputIdentifyDeleteCustomer)
    {
        var response = new BaseResponse<bool>();
        var listOriginal = await _customerRepository.GetListByListId(listInputIdentifyDeleteCustomer.Select(i => i.Id).ToList());
        var listRepeatedDelete = (from i in listInputIdentifyDeleteCustomer
                                  where listInputIdentifyDeleteCustomer.Count(j => j.Id == i.Id) > 1
                                  select i).ToList();
        var listDelete = (from i in listInputIdentifyDeleteCustomer
                          select new
                          {
                              InputDeleteCustomer = i,
                              Original = listOriginal.FirstOrDefault(j => j.Id == i.Id).ToCustomerDTO(),
                              RepeatedDelete = listRepeatedDelete.FirstOrDefault(k => k.Id == i.Id)
                          });

        List<CustomerValidate> customerValidate = listDelete.Select(i => new CustomerValidate().ValidateDelete(i.InputDeleteCustomer, i.Original, i.RepeatedDelete)).ToList();
        var deleteValidate = await _customerValidateService.ValidateDelete(customerValidate);
        response.Success = deleteValidate.Success;
        response.Message = deleteValidate.Message;
        if (!response.Success)
        {
            response.Content = false;
            return response;
        }

        var listDeleteCustomer = await _customerRepository.GetListByListId((deleteValidate.Content.Select(i => i.Original.Id)).ToList());
        response.Content = await _customerRepository.Delete(listDeleteCustomer);
        return response;
    }
    #endregion

}