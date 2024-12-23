using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProjetoTeste.Infrastructure.Persistence.Entity;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Context;

namespace ProjetoTeste.Infrastructure.Persistence.Repository;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction();
    private readonly AppDbContext _context;
    public async Task Commit()
    {
        await context.Database.CommitTransactionAsync();
    }
}