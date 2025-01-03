using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Interface.Repositories;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity;

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

        public async Task<TEntity?> Get(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity?> Create(TEntity? entity)
        {
            await _dbSet.AddAsync(entity);
            if (entity == null) return entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> Update(TEntity? entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public async Task<bool> Delete(long id)
        {
            var entityRemove = await _dbSet.FindAsync(id);
            _dbSet.Remove(entityRemove);
            return true;
        }
        public async void SaveChances()
        {
            await _context.SaveChangesAsync();
        }
    }
}