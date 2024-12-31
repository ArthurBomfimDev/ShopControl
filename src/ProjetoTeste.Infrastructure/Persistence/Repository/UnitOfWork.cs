using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Context;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IDbContextTransaction dbContextTransaction;
    private readonly AppDbContext _context = context;
    public async Task BeginTransactionAsync()
    {
        if (dbContextTransaction == null)
        {
            dbContextTransaction = await _context.Database.BeginTransactionAsync();
        }
    }
    //public async Task CommitAsync()
    //{
    //    await _context.SaveChangesAsync();
    //    await dbContextTransaction.CommitAsync();
    //}
    public async Task CommitAsync()
    {
        if (dbContextTransaction != null)
        {
            await _context.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();
        }
    }
}