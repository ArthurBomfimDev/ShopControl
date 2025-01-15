using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Base;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System.Text.RegularExpressions;

namespace ProjetoTeste.Infrastructure.Application;

public class CustomerValidateService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerValidateService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    #region CPFValidate
    public bool CPFValidate(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        cpf = Regex.Replace(cpf, "[^0-9]", string.Empty);

        if (cpf.Length != 11) return false;

        if (new string(cpf[0], cpf.Length) == cpf) return false;

        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        int soma = 0;
        for (int i = 0; i < 9; i++)
        {
            soma += (cpf[i] - '0') * multiplicadores1[i];
        }

        int resto = soma % 11;
        int primeiroDigitoVerificador = resto < 2 ? 0 : 11 - resto;

        if (cpf[9] - '0' != primeiroDigitoVerificador) return false;

        soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += (cpf[i] - '0') * multiplicadores2[i];
        }

        resto = soma % 11;
        int segundoDigitoVerificador = resto < 2 ? 0 : 11 - resto;

        if (cpf[10] - '0' != segundoDigitoVerificador) return false;

        return true;
    }
    #endregion

    #region Create
    public async Task<BaseResponse<List<CustomerValidate>>> ValidateCreate(List<CustomerValidate> listCustomerValidate)
    {
        var response = new BaseResponse<List<CustomerValidate>>();

        _ = (from i in listCustomerValidate
             where i.InputCreateCustomer.Name.Length > 64
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where i.InputCreateCustomer.Email.Length > 64
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where CPFValidate(i.InputCreateCustomer.CPF) == false
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where i.InputCreateCustomer.Phone.Length > 15
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        var create = (from i in listCustomerValidate
                      where !i.Invalid
                      let message = response.AddSuccessMessage("")
                      select i).ToList();

        if (!create.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = create;
        return response;
        #region ValidateUnic
        ////var cpfExistsList = (from i in inputCreateList
        ////                     where _customerRepository.CPFExists(i.CPF) == true
        ////                     select i).ToList();

        ////if (cpfExistsList.Count > 0)
        ////{
        ////    foreach (var customer in cpfExistsList)
        ////    {
        ////        response.Message.Add(new Notification { Message = $" >>> CPF: {customer.CPF} do Cliente: {customer.Name} já está cadastrado <<< ", Type = EnumNotificationType.Error });
        ////    }
        ////    inputCreateList = (inputCreateList.Except(cpfExistsList).ToList());
        ////}

        ////if (inputCreateList.Count == 0)
        ////{
        ////    response.Success = false;
        ////    return response;
        ////}
        ////var emailExistsList = (from i in inputCreateList
        ////                       where _customerRepository.EmailExists(i.Email) == true
        ////                       select i).ToList();

        ////if (emailExistsList.Count > 0)
        ////{
        ////    foreach (var customer in emailExistsList)
        ////    {
        ////        response.Message.Add(new Notification { Message = $" >>> Email: {customer.Email} do Cliente: {customer.Name} já está cadastrado <<< ", Type = EnumNotificationType.Error });
        ////    }
        ////    inputCreateList = (inputCreateList.Except(emailExistsList).ToList());
        ////}

        ////if (inputCreateList.Count == 0)
        ////{
        ////    response.Success = false;
        ////    return response;
        ////}
        ////var phoneExistsList = (from i in inputCreateList
        ////                       where _customerRepository.PhoneExists(i.Phone) == true
        ////                       select i).ToList();

        ////if (phoneExistsList.Count > 0)
        ////{
        ////    foreach (var customer in phoneExistsList)
        ////    {
        ////        response.Message.Add(new Notification { Message = $" >>> Phone: {customer.Phone} do Cliente: {customer.Name} já está cadastrado <<< ", Type = EnumNotificationType.Error });
        ////    }
        ////    inputCreateList = (inputCreateList.Except(phoneExistsList).ToList());
        ////}
        #endregion
    }
    #endregion

    #region Update
    public async Task<BaseResponse<List<CustomerValidate>>> ValidateUpdate(List<CustomerValidate> listCustomerValidate)
    {
        var response = new BaseResponse<List<CustomerValidate>>();

        _ = (from i in listCustomerValidate
             where i.RepeteId != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where i.Original == null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name.Length > 64
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email.Length > 64
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where CPFValidate(i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF) == false
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        _ = (from i in listCustomerValidate
             where i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone.Length > 15
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage("")
             select i).ToList();

        var update = (from i in listCustomerValidate
                      where !i.Invalid
                      let message = response.AddSuccessMessage("")
                      select i).ToList();

        if (!update.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = update;
        return response;

        //if (idList.Count != inputUpdateList.Count)
        //{
        //    response.Success = false;
        //    response.AddErrorMessage(" >>> ERRO - A Quantidade de Id's Digitados é Diferente da Quantdade de Marcas <<<");
        //    return response;
        //}

        //var notExists = (from i in idList
        //                 where _customerRepository.Exists(i) == false
        //                 select idList.IndexOf(i)).ToList();

        //if (notExists.Count > 0)
        //{
        //    for (int i = 0; i < notExists.Count; i++)
        //    {
        //        response.AddErrorMessage($" >>> O Cliente com id: {inputUpdateList[notExists[i]]} não existe <<<");
        //        idList.Remove(idList[notExists[i]]);
        //        inputUpdateList.Remove(inputUpdateList[notExists[i]]);
        //    }
        //}

        //if (inputUpdateList.Count == 0)
        //{
        //    response.Success = false;
        //    return response;
        //}

        //var cpfExists = (from i in inputUpdateList
        //                 where CPFValidate(i.CPF) == false
        //                 select i).ToList();
        //if (cpfExists.Count > 0)
        //{
        //    foreach (var customer in cpfExists)
        //    {
        //        response.AddErrorMessage($" >>> Cliente: {customer.Name} com CPF: {customer.CPF} é inválido <<< ");
        //        var index = cpfExists.IndexOf(customer);
        //        idList.RemoveAt(index);
        //    }
        //    inputUpdateList = inputUpdateList.Except(cpfExists).ToList();
        //}

        //if (inputUpdateList.Count == 0) { response.Success = false; return response; }

        //var existingCustomer = await _customerRepository.GetListByListId(idList);

        //for (int i = 0; i < existingCustomer.Count; i++)
        //{
        //    existingCustomer[i].Name = inputUpdateList[i].Name;
        //    existingCustomer[i].CPF = inputUpdateList[i].CPF;
        //    existingCustomer[i].Email = inputUpdateList[i].Email;
        //    existingCustomer[i].Phone = inputUpdateList[i].Phone;
        //}

        //response.Content = existingCustomer;
        //return response;
    }
    #endregion

    #region Delete
    public async Task<BaseResponse<List<Customer>>> ValidateDelete(List<long> idList)
    {
        var response = new BaseResponse<List<Customer>>();
        var idExists = (from i in idList
                        where _customerRepository.Exists(i) == false
                        select i).ToList();

        if (idExists.Count > 0)
        {
            foreach (var id in idExists)
            {
                response.AddErrorMessage($" >>> O Cliente com Id: {id} não Existe <<<");
            }
            idList = idList.Except(idExists).ToList();
        }

        if (idList.Count() == 0)
        {
            response.Success = false;
            return response;
        }

        var customerList = await _customerRepository.GetListByListId(idList);
        response.Content = customerList;
        return response;
    }
    #endregion
}