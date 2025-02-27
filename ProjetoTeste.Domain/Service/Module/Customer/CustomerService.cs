using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository;
using ProjetoTeste.Domain.Interface.Service;
using ProjetoTeste.Domain.Service.Base;
using ProjetoTeste.Infrastructure.Interface.ValidateService;

namespace ProjetoTeste.Domain.Service;

public class CustomerService : BaseService<ICustomerRepository, ICustomerValidateService, CustomerDTO, InputCreateCustomer, InputIdentityUpdateCustomer, InputIdentifyDeleteCustomer, InputIdentifyViewCustomer, OutputCustomer, CustomerValidateDTO>, ICustomerService
{
    #region Dependency Injection
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerValidateService _customerValidateService;

    public CustomerService(ICustomerRepository customerRepository, ICustomerValidateService customerValidateService) : base(customerRepository, customerValidateService)
    {
        _customerRepository = customerRepository;
        _customerValidateService = customerValidateService;
    }
    #endregion

    #region Create
    public override async Task<BaseResult<List<OutputCustomer>>> CreateMultiple(List<InputCreateCustomer> listInputCreateCustomer)
    {
        List<CustomerValidateDTO> listCutomerValidate = listInputCreateCustomer.Select(i => new CustomerValidateDTO().ValidateCreate(i)).ToList();

        _customerValidateService.ValidateCreate(listCutomerValidate);

        var (success, errors) = GetValidationResult();
        if (success.Count == 0)
            return BaseResult<List<OutputCustomer>>.Failure(errors);

        var listCreate = (from i in listCutomerValidate
                          select new CustomerDTO(i.InputCreateCustomer.Name, i.InputCreateCustomer.CPF, i.InputCreateCustomer.Email, i.InputCreateCustomer.Phone)).ToList();

        var create = await _customerRepository.Create(listCreate);

        return BaseResult<List<OutputCustomer>>.Success(create.Select(i => (OutputCustomer)i).ToList(), [.. success, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<bool>> UpdateMultiple(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer)
    {
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

        List<CustomerValidateDTO> listCustomerValidate = listUpdate.Select(i => new CustomerValidateDTO().ValidateUpdate(i.InputIdentityUpdateCustomer, i.OriginalCustomer, i.RepeteId)).ToList();
        _customerValidateService.ValidateUpdate(listCustomerValidate);

        var (succes, errors) = GetValidationResult();
        if (succes.Count == 0)
            return BaseResult<bool>.Failure(errors);

        var listUpdateCustomer = (from i in listCustomerValidate
                                  let name = i.OriginalDTO.Name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
                                  let cpf = i.OriginalDTO.CPF = i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF
                                  let email = i.OriginalDTO.Email = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email
                                  let phone = i.OriginalDTO.Phone = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone
                                  select i.OriginalDTO).ToList();

        await _customerRepository.Update(listUpdateCustomer);
        return BaseResult<bool>.Success(true, [.. succes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> DeleteMultiple(List<InputIdentifyDeleteCustomer> listInputIdentifyDeleteCustomer)
    {
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

        List<CustomerValidateDTO> listCustomerValidate = listDelete.Select(i => new CustomerValidateDTO().ValidateDelete(i.InputDeleteCustomer, i.Original, i.RepeatedDelete)).ToList();
        _customerValidateService.ValidateDelete(listCustomerValidate);

        var (succes, errors) = GetValidationResult();
        if (succes.Count == 0)
            return BaseResult<bool>.Failure(errors);

        var listDeleteCustomer = (from i in listCustomerValidate
                                  select i.OriginalDTO).ToList();

        await _customerRepository.Delete(listDeleteCustomer);

        return BaseResult<bool>.Success(true, [.. succes, .. errors]);
    }
    #endregion
}