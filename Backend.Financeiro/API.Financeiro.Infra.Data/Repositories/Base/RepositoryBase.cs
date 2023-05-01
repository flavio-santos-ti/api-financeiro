using Api.Crud.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Crud.Infra.Data.Repositories.Base;

public class RepositoryBase 
{
    private readonly DatabaseContext _context;

    public RepositoryBase(DatabaseContext context) => _context = context;

    protected async Task AddAsync<T>(T dados) where T : class => await _context.Set<T>().AddAsync(dados);

    protected async Task<T> UpdateAsync<T>(T dados) where T : class
    {
        await Task.Run(() => _context.Set<T>().Update(dados) );

        return dados;
    }
    protected async Task<int> DeleteAsync<T>(T dados) where T : class
    {
        await Task.Run(() => _context.Set<T>().Remove(dados) );
        return 1;
    }

    protected async Task<T> GetAsync<T>(Expression<Func<T, bool>> condicao) where T : class
    {
        return await _context.Set<T>().Where(condicao).FirstOrDefaultAsync();
    }


    protected async Task<ICollection<T>> GetAllAsync<T>(Expression<Func<T, bool>> condicao) where T : class
    {
        return await _context.Set<T>().Where(condicao).AsNoTracking().ToListAsync();
    }

    protected DatabaseContext GetContext()
    {
        return _context;
    }
}
