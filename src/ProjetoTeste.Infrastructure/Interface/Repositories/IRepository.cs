using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Interface.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity, new()
{
    Task<List<TEntity?>> GetAll();
    Task<List<TEntity>> GetListByListId(List<long> ids);
    Task<TEntity?> Get(long id);
    //Task<TEntity>? Create(TEntity entity);
    Task<List<TEntity>?> Create(List<TEntity> entityList);
    //Task<bool> Update(TEntity entity);
    Task<bool> Update(List<TEntity> entityList);
    //Task<bool> Delete(TEntity entity);
    Task<bool> Delete(List<TEntity> entityList);
}