namespace Api.Crud.Infra.Data.Interfaces;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RolbackAsync();
    Task<int> SaveAsync();
    string GetTokenSecret();
}
