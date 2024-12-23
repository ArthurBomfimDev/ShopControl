using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProjetoEstagioAPI.Context;
using ProjetoEstagioAPI.Infrastructure.Brands;
using ProjetoEstagioAPI.Infrastructure.Clients;
using ProjetoEstagioAPI.Infrastructure.Orders;
using ProjetoEstagioAPI.Infrastructure.Products;
using ProjetoEstagioAPI.Models;

namespace ProjetoEstagioAPI.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextTransaction dbContextTransaction = Context.Database.BeginTransaction();
        private readonly AppContext _context;
        public async Task Commit()
        {
           await context.Database.Transaction
        }
    }
}