using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Enum.Validate;
using ProjetoTeste.Domain.Helper;
using ProjetoTeste.Domain.Service.Base;
using ProjetoTeste.Infrastructure.Interface.ValidateService;

namespace ProjetoTeste.Infrastructure.Application;

public class CustomerValidateService : BaseValidate<CustomerValidateDTO>, ICustomerValidateService
{
    #region Create
    public void ValidateCreate(List<CustomerValidateDTO> listCustomerValidate)
    {
        (from i in listCustomerValidate
         where i.InputCreateCustomer == null
         let setInvalid = i.SetIgnore()
         select i).ToList();

        (from i in RemoveIgnore(listCustomerValidate)
         let cpfValidate = CPFValidate(i.InputCreateCustomer.CPF)
         where cpfValidate != EnumValidateType.Valid
         let setInvalid = cpfValidate == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidEmail(i.InputCreateCustomer.CPF, i.InputCreateCustomer.CPF)).ToList();

        (from i in RemoveIgnore(listCustomerValidate)
         let resultInvalidLenght = InvalidLenght(i.InputCreateCustomer.Name, 1, 64)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidLenght(i.InputCreateCustomer.CPF, i.InputCreateCustomer.Name, 1, 64, resultInvalidLenght, "Nome")).ToList();

        (from i in RemoveIgnore(listCustomerValidate)
         let emailValidate = EmailValidate(i.InputCreateCustomer.Email)
         where emailValidate != EnumValidateType.Valid
         let setInvalid = i.SetInvalid()
         select InvalidEmail(i.InputCreateCustomer.CPF, i.InputCreateCustomer.Email)).ToList();


        (from i in RemoveIgnore(listCustomerValidate)
         let resultPheneValidate = PhoneValidate(i.InputCreateCustomer.Phone)
         where resultPheneValidate != EnumValidateType.Valid
         let setInvalid = resultPheneValidate == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidPhone(i.InputCreateCustomer.CPF, i.InputCreateCustomer.Phone)).ToList();

        (from i in RemoveInvalid(listCustomerValidate)
         select SuccessfullyRegistered(i.InputCreateCustomer.CPF, "Cliente")).ToList();

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
    public void ValidateUpdate(List<CustomerValidateDTO> listCustomerValidate)
    {

        NotificationHelper.CreateDict();

        (from i in RemoveIgnore(listCustomerValidate)
         where i.RepeteId != 0
         let setInvalid = i.SetInvalid()
         select RepeatedId(i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF, i.InputIdentityUpdateCustomer.Id)).ToList();

        (from i in RemoveIgnore(listCustomerValidate)
         where i.OriginalDTO == null
         let setInvalid = i.SetInvalid()
         select NotFoundId(i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF, "Cliente", i.InputIdentityUpdateCustomer.Id)).ToList();

        (from i in RemoveIgnore(listCustomerValidate)
         let cpfValidate = CPFValidate(i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF)
         where cpfValidate != EnumValidateType.Valid
         let setInvalid = cpfValidate == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidCPF(i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF, i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF)).ToList();

        (from i in RemoveIgnore(listCustomerValidate)
         let resultInvalidLenght = InvalidLenght(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name, 1, 64)
         where resultInvalidLenght != EnumValidateType.Valid
         let setInvalid = resultInvalidLenght == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidLenght(i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF, i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name, 1, 64, resultInvalidLenght, "Nome")).ToList();

        (from i in RemoveIgnore(listCustomerValidate)
         let emailValidate = EmailValidate(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email)
         where emailValidate != EnumValidateType.Valid
         let setInvalid = i.SetInvalid()
         select InvalidEmail(i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF, i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email)).ToList();


        (from i in RemoveIgnore(listCustomerValidate)
         let resultPheneValidate = PhoneValidate(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone)
         where resultPheneValidate != EnumValidateType.Valid
         let setInvalid = resultPheneValidate == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
         select InvalidPhone(i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF, i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone)).ToList();

        (from i in RemoveInvalid(listCustomerValidate)
         select SuccessfullyUpdated(i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF, i.InputIdentityUpdateCustomer.Id, "Cliente")).ToList();

        #region Unique
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
        #endregion
    }
    #endregion

    #region Delete
    public void ValidateDelete(List<CustomerValidateDTO> listCustomerValidate)
    {
        NotificationHelper.CreateDict();

        (from i in RemoveIgnore(listCustomerValidate)
         where i.RepeatedDelete != null
         let setInvalid = i.SetInvalid()
         select RepeatedId(i.InputIdentifyDeleteCustomer.Id.ToString(), i.InputIdentifyDeleteCustomer.Id)).ToList();

        (from i in RemoveIgnore(listCustomerValidate)
         where i.OriginalDTO == null
         let setInvalid = i.SetInvalid()
         select NotFoundId(i.InputIdentifyDeleteCustomer.Id.ToString(), "Cliente", i.InputIdentifyDeleteCustomer.Id)).ToList();

        (from i in RemoveInvalid(listCustomerValidate)
         select SuccessfullyDeleted(i.InputIdentifyDeleteCustomer.Id.ToString(), "Cliente")).ToList();
    }
    #endregion
}