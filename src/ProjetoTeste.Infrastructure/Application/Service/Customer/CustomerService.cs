using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Application.Service.Base;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.ValidateService;
using ProjetoTeste.Infrastructure.Persistence.Entity;

namespace ProjetoTeste.Infrastructure.Application;

public class CustomerService : BaseService<ICustomerRepository, Customer, InputCreateCustomer, InputIdentityUpdateCustomer, InputIdentifyDeleteCustomer, InputIdentifyViewCustomer, OutputCustomer>, ICustomerService
{
    #region Dependency Injection
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerValidateService _customerValidateService;

    public CustomerService(ICustomerRepository customerRepository, ICustomerValidateService customerValidateService) : base(customerRepository)
    {
        _customerRepository = customerRepository;
        _customerValidateService = customerValidateService;
    }
    #endregion

    #region Create
    public override async Task<BaseResponse<List<OutputCustomer>>> CreateMultiple(List<InputCreateCustomer> listInputCreateCustomer)
    {
        var response = new BaseResponse<List<OutputCustomer>>();

        List<CustomerValidate> listCutomerValidate = listInputCreateCustomer.Select(i => new CustomerValidate().ValidateCreate(i)).ToList();

        var validateCreate = await _customerValidateService.ValidateCreate(listCutomerValidate);

        response.Success = validateCreate.Success;
        response.Message = validateCreate.Message;
        if (!response.Success) return response;

        var listCreate = (from i in validateCreate.Content
                          let message = response.AddSuccessMessage($"O cliente: '{i.InputCreateCustomer.Name}' foi cadastrado com sucesso.")
                          select new Customer(i.InputCreateCustomer.Name, i.InputCreateCustomer.CPF, i.InputCreateCustomer.Email, i.InputCreateCustomer.Phone)).ToList();

        var create = await _customerRepository.Create(listCreate);
        response.Content = create.Select(i => (OutputCustomer)(CustomerDTO)i).ToList();
        return response;
    }
    #endregion

    #region Update
    public override async Task<BaseResponse<bool>> UpdateMultiple(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer)
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
                              OriginalCustomer = originalCustomer.FirstOrDefault(j => j.Id == i.Id),
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
                                  let name = i.OriginalDTO.Name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
                                  let cpf = i.OriginalDTO.CPF = i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF
                                  let email = i.OriginalDTO.Email = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email
                                  let phone = i.OriginalDTO.Phone = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone
                                  let message = response.AddSuccessMessage($"O cliente com o ID: '{i.InputIdentityUpdateCustomer.Id}' foi atualizado com sucesso.")
                                  select (Customer)i.OriginalDTO).ToList();

        response.Content = await _customerRepository.Update(listUpdateCustomer);
        return response;
    }
    #endregion

    #region Delete
    public override async Task<BaseResponse<bool>> DeleteMultiple(List<InputIdentifyDeleteCustomer> listInputIdentifyDeleteCustomer)
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
                              Original = listOriginal.FirstOrDefault(j => j.Id == i.Id),
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

        var listDeleteCustomer = (from i in deleteValidate.Content
                                  let message = response.AddSuccessMessage($"Cliente com ID: {i.InputIdentifyDeleteCustomer.Id} foi excluído com sucesso.")
                                  select (Customer)i.OriginalDTO).ToList();

        response.Content = await _customerRepository.Delete(listDeleteCustomer);
        return response;
    }
    #endregion

}