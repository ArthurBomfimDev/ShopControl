using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity?>();
        }
        public async Task<List<TEntity?>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public List<TEntity?> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<List<TEntity>> Get(List<long> ids)
        {
            var entityList = new List<TEntity>();
            foreach (var id in ids)
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null) entityList.Add(entity);
            }
            return entityList;
        }

        public async Task<List<TEntity>?> Create(List<TEntity>? entityList)
        {
            await _dbSet.AddRangeAsync(entityList);
            await _context.SaveChangesAsync();
            return entityList;
        }

        public async Task<bool> Update(List<TEntity>? entityList)
        {
            _dbSet.UpdateRange(entityList);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(List<TEntity> entityList)
        {
            _dbSet.RemoveRange(entityList);
            return true;
        }
        public async void SaveChances()
        {
            await _context.SaveChangesAsync();
        }
    }
}