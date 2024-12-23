using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Context;
using ProjetoTeste.Infrastructure.Default;
using ProjetoTeste.Models;

namespace ProjetoTeste.Infrastructure.Brands;

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