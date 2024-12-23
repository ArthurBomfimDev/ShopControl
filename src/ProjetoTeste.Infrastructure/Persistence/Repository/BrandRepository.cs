using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Persistence.Context;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class BrandRepository : Repository<Brand>, IBrandRepository
{
    public BrandRepository(AppDbContext context) : base(context)
    {
    }

    public Task<bool> Exist(string name)
    {
        var exists = _dbSet.AnyAsync(x => x.Name == name);
        return exists;
    }
}