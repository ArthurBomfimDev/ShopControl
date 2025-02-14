namespace ProjetoTeste.Infrastructure.Interface.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity?>> GetAll();
    Task<List<TEntity>> GetListByListId(List<long>? listId);
    Task<TEntity?> Get(long id);
    Task<List<TEntity>?> Create(List<TEntity> listEntity);
    Task<bool> Update(List<TEntity> listEntity);
    Task<bool> Delete(List<TEntity> listEntity);
}