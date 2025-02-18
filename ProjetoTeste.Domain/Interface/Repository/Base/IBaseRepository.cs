using ProjetoTeste.Domain.DTO.Base;

namespace ProjetoTeste.Domain.Interface.Repository.Base;

public interface IBaseRepository<TDTO> where TDTO : BaseDTO<TDTO>
{
    Task<List<TDTO?>> GetAll();
    Task<List<TDTO>> GetListByListId(List<long>? listId);
    Task<TDTO?> Get(long id);
    Task<List<TDTO>?> Create(List<TDTO> listEntity);
    Task<bool> Update(List<TDTO> listEntity);
    Task<bool> Delete(List<TDTO> listEntity);
}