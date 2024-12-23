using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProjetoTeste.Context;
using ProjetoTeste.Infrastructure.Brands;
using ProjetoTeste.Infrastructure.Clients;
using ProjetoTeste.Infrastructure.Orders;
using ProjetoTeste.Infrastructure.Products;

namespace ProjetoTeste.Infrastructure.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction();
    private readonly AppDbContext _context;
    public async Task CommitAsync()
    {
        await context.Database.CommitTransactionAsync();
    }
}