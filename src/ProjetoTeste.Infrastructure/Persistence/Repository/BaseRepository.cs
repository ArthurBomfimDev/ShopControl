using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Arguments.Conversor;
using ProjetoTeste.Domain.DTO.Base;
using ProjetoTeste.Domain.Interface.Repository.Base;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entity.Base;

namespace ProjetoTeste.Infrastructure.Persistence.Repository
{
    public abstract class BaseRepository<TEntity, TDTO> : IBaseRepository<TDTO>
        where TEntity : BaseEntity
        where TDTO : BaseDTO<TDTO>
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity?>();
        }
        public async Task<List<TDTO?>> GetAll()
        {
            var getAll = await _dbSet.ToListAsync();
            return getAll?.ConverterList<TEntity, TDTO>();
        }

        public async Task<List<TDTO>> GetListByListId(List<long> listId)
        {
            var getListByListId = await _dbSet.Where(i => listId.Contains(i.Id)).AsNoTracking().ToListAsync();
            return getListByListId.ConverterList<TEntity, TDTO>();
        }

        public async Task<TDTO?> Get(long id)
        {
            var get = await _dbSet.FindAsync(id);
            return get.ConverterReflection<TEntity, TDTO>();
        }

        public async Task<List<TDTO>?> Create(List<TDTO>? listDTO)
        {
            var listEntity = listDTO?.ConverterList<TDTO, TEntity>();
            await _dbSet.AddRangeAsync(listEntity);
            await _context.SaveChangesAsync();
            return listEntity.ConverterList<TEntity, TDTO>();
        }

        public async Task<bool> Update(List<TDTO>? listDTO)
        {
            var listEntity = listDTO?.ConverterList<TDTO, TEntity>();
            _dbSet.UpdateRange(listEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(List<TDTO> listDTO)
        {
            var listEntity = listDTO?.ConverterList<TDTO, TEntity>();
            _dbSet.RemoveRange(listEntity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}