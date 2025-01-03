namespace ProjetoTeste.Infrastructure.Interface.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T?>> GetAllAsync();
    List<T?> GetAll();
    Task<T?> Get(long id);
    Task<T?> Create(T entity);
    Task<T?> Update(T entity);
    Task<bool> Delete(long id);
}
