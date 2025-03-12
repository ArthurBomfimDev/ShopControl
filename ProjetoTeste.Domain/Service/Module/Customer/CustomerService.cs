using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Base.ApiResponse;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Domain.DTO;
using ProjetoTeste.Domain.Interface.Repository;
using ProjetoTeste.Domain.Interface.Service;
using ProjetoTeste.Domain.Service.Base;
using ProjetoTeste.Infrastructure.Interface.ValidateService;

namespace ProjetoTeste.Domain.Service;

public class CustomerService : BaseService<ICustomerRepository, ICustomerValidateService, CustomerDTO, InputCreateCustomer, InputUpdateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer, InputIdentifyViewCustomer, OutputCustomer, CustomerValidateDTO>, ICustomerService
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
    public override async Task<BaseResult<List<OutputCustomer>>> CreateMultiple(List<InputCreateCustomer> listInputCreate)
    {
        var dictionaryLength = _customerRepository.DictionaryLength();

        List<CustomerValidateDTO> listCutomerValidate = listInputCreate.Select(i => new CustomerValidateDTO().ValidateCreate(i, dictionaryLength)).ToList();

        _customerValidateService.ValidateCreate(listCutomerValidate);

        var listNotification = GetAllNotification();

        if (listNotification!.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<List<OutputCustomer>>.Failure(listNotification!);

        var listCreate = (from i in listCutomerValidate
                          select new CustomerDTO(i.InputCreate.Name, i.InputCreate.CPF, i.InputCreate.Email, i.InputCreate.Phone)).ToList();

        var create = await _customerRepository.Create(listCreate);

        return BaseResult<List<OutputCustomer>>.Success(create.Select(i => (OutputCustomer)i).ToList(), listNotification!);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<bool>> UpdateMultiple(List<InputIdentityUpdateCustomer> listInputIdentityUpdate)
    {
        var originalCustomer = await _customerRepository.GetListByListId(listInputIdentityUpdate.Select(i => i.Id).ToList());
        var listRepeteId = (from i in listInputIdentityUpdate
                            where listInputIdentityUpdate.Count(j => j.Id == i.Id) > 1
                            select i.Id).ToList();

        var listUpdate = (from i in listInputIdentityUpdate
                          select new
                          {
                              InputIdentityUpdate = i,
                              OriginalCustomer = originalCustomer.FirstOrDefault(j => j.Id == i.Id),
                              RepeteId = listRepeteId.FirstOrDefault(k => k == i.Id),
                          }).ToList();

        var dictionaryLength = _customerRepository.DictionaryLength();


        List<CustomerValidateDTO> listCustomerValidate = listUpdate.Select(i => new CustomerValidateDTO().ValidateUpdate(i.InputIdentityUpdate, i.OriginalCustomer, i.RepeteId, dictionaryLength)).ToList();
        _customerValidateService.ValidateUpdate(listCustomerValidate);

        var listNotification = GetAllNotification();

        if (listNotification!.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<bool>.Failure(listNotification!);

        var listUpdateCustomer = (from i in listCustomerValidate
                                  let name = i.OriginalDTO.Name = i.InputIdentityUpdate.InputUpdate.Name
                                  let cpf = i.OriginalDTO.CPF = i.InputIdentityUpdate.InputUpdate.CPF
                                  let email = i.OriginalDTO.Email = i.InputIdentityUpdate.InputUpdate.Email
                                  let phone = i.OriginalDTO.Phone = i.InputIdentityUpdate.InputUpdate.Phone
                                  select i.OriginalDTO).ToList();

        await _customerRepository.Update(listUpdateCustomer);
        return BaseResult<bool>.Success(true, listNotification!);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> DeleteMultiple(List<InputIdentityDeleteCustomer> listInputIdentifyDeleteCustomer)
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

        var listNotification = GetAllNotification();

        if (listNotification!.Where(i => i.NotificationType == EnumNotificationType.Success).ToList().Count == 0)
            return BaseResult<bool>.Failure(listNotification!);

        var listDeleteCustomer = (from i in listCustomerValidate
                                  select i.OriginalDTO).ToList();

        await _customerRepository.Delete(listDeleteCustomer);

        return BaseResult<bool>.Success(true, listNotification!);
    }
    #endregion
}