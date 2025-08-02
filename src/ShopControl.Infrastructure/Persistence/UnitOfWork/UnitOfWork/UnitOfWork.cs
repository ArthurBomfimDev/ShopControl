using Microsoft.EntityFrameworkCore.Storage;
using ShopControl.Infrastructure.Interface.UnitOfWork;
using ShopControl.Infrastructure.Persistence.Context;

namespace ShopControl.Infrastructure.Persistence;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IDbContextTransaction dbContextTransaction;
    private readonly AppDbContext _context = context;
    public void BeginTransaction()
    {
        dbContextTransaction = _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _context.SaveChanges();
        dbContextTransaction.Commit();
        dbContextTransaction.Dispose();
    }
}