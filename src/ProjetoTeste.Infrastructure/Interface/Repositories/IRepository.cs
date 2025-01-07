using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Interface.Repositories;
//Trocar os create update e delete pra list
public interface IRepository<TEntity> where TEntity : BaseEntity, new()
{
    Task<List<TEntity?>> GetAllAsync();
    List<TEntity?> GetAll();
    Task<TEntity?> Get(long id);
    Task<TEntity?> Create(List<TEntity> entity);
    Task<TEntity?> Update(List<TEntity> entity);
    Task<bool> Delete(long id);
    List<TEntity>? Update(List<TEntity>? entity);
}