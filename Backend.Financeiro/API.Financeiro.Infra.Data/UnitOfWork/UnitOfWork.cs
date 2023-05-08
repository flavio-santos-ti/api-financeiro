using Api.Crud.Infra.Data.Context;
using Api.Crud.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Api.Crud.Infra.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(DatabaseContext context) => _context = context;

    public async Task BeginTransactionAsync() => _transaction = await _context.Database.BeginTransactionAsync();

    public async Task CommitAsync() => await _transaction.CommitAsync();

    public async Task RolbackAsync() => await _transaction.RollbackAsync();

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public string GetTokenSecret()
    {
        var configuration = _context.GetConfiguration();
        return configuration.GetSection("TokenConfiguration").GetValue<string>("TokenSecret");
    }
}
