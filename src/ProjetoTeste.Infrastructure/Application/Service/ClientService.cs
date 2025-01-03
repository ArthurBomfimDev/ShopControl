using ProjetoTeste.Arguments.Arguments.Client;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using System.Text.RegularExpressions;

namespace ProjetoTeste.Infrastructure.Application.Service;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<List<OutputClient>>> GetAll()
    {
        var clientList = await _clientRepository.GetAllAsync();
        return new Response<List<OutputClient>>() { Success = true, Value = (from i in clientList select i.ToOutputClient()).ToList() };
    }

    public async Task<Response<OutputClient>> Get(long id)
    {
        var client = await _clientRepository.Get(id);
        return new Response<OutputClient> { Success = true, Value = client.ToOutputClient() };
    }

    public async Task<Response<OutputClient>> Create(InputCreateClient client)
    {
        var response = new Response<OutputClient>();
        if (client is null)
        {
            response.Success = false;
            response.Message.Add(" >>> Dados Inseridos Inválidos <<< ");
        }
        if (await _clientRepository.CPFExists(client.CPF))
        {
            response.Success = false;
            response.Message.Add(" >>> CPF Já Cadastrado <<< ");
        }
        if (await _clientRepository.EmailExists(client.Email))
        {
            response.Success = false;
            response.Message.Add(" >>> Email Já Cadastrado <<< ");
        }
        if (await _clientRepository.PhoneExists(client.Phone))
        {
            response.Success = false;
            response.Message.Add(" >>> Número de telefone Já Cadastrado <<< ");
        }
        if (!CpfValidate(client.CPF))
        {
            response.Success = false;
            response.Message.Add(" >>> Digite um CPF válido <<< ");
        }
        if (!(client.Phone.Length == 11))
        {
            response.Success = false;
            response.Message.Add(" >>> Digite um número de Telefone válido <<< ");
        }
        if (!response.Success)
        {
            return response;
        }
        var newClient = client.ToClient();
        var createClient = await _clientRepository.Create(newClient);
        return new Response<OutputClient> { Success = true, Value = createClient.ToOutputClient() };
    }

    public async Task<Response<bool>> Update(long id, InputUpdateClient client)
    {
        var clientExists = await _clientRepository.Get(id);
        var response = new Response<bool>();
        if (clientExists == null)
        {
            return new Response<bool>() { Success = false, Message = { " >>> Cliente com o Id digitado NÃO encontrado <<<" } };
        }
        if (client is null)
        {
            return new Response<bool>() { Success = false, Message = { " >>> Dados Inválidos <<<" } };

        }
        if (!string.Equals(client.CPF, clientExists.CPF, StringComparison.OrdinalIgnoreCase))
        {
            if (await _clientRepository.CPFExists(client.CPF))
            {
                response.Success = false;
                response.Message.Add(" >>> CPF Já Cadastrado <<< ");
            }
        }
        if (!string.Equals(client.Email, clientExists.Email, StringComparison.OrdinalIgnoreCase))
        {
            if (await _clientRepository.EmailExists(client.Email))
            {
                response.Success = false;
                response.Message.Add(" >>> Email Já Cadastrado <<< ");
            }
        }
        if (!string.Equals(client.Phone, clientExists.Phone, StringComparison.OrdinalIgnoreCase))
        {
            if (await _clientRepository.PhoneExists(client.Phone))
            {
                response.Success = false;
                response.Message.Add(" >>> Número de Telefone Já Cadastrado <<< ");
            }
        }
        if (!CpfValidate(client.CPF))
        {
            response.Success = false;
            response.Message.Add(" >>> Digite um CPF válido <<< ");
        }
        if (!(client.Phone.Length == 11))
        {
            response.Success = false;
            response.Message.Add(" >>> Digite um número de Telefone válido <<< ");
        }
        if (!response.Success)
        {
            return response;
        }
        clientExists.Email = client.Email;
        clientExists.Phone = client.Phone;
        clientExists.CPF = client.CPF;
        clientExists.Name = client.Name;
        _clientRepository.Update(clientExists);
        return new Response<bool> { Success = true, Message = { "Cliente Atualizado com SUCESSO" } };
    }

    public async Task<Response<bool>> Delete(long id)
    {
        var clientExists = await _clientRepository.Get(id);
        if (clientExists == null)
        {
            return new Response<bool>() { Success = false, Message = { " >>> Cliente com o Id digitado NÃO encontrado <<<" } };
        }
        await _clientRepository.Delete(id);
        return new Response<bool>() { Success = true, Message = { " >>> Cliente deletado com SUCESSO <<<" } };
    }

    public bool CpfValidate(string cpf)
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

}
