using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Interface.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity, new()
{
    Task<List<TEntity?>> GetAllAsync();
    List<TEntity?> GetAll();
    Task<TEntity?> Get(long id);
    Task<TEntity?> Create(TEntity entity);
    Task<TEntity?> Update(TEntity entity);
    Task<bool> Delete(long id);
}