using API.Financeiro.Domain.Categoria;
using System.Linq.Expressions;

namespace API.Financeiro.Infra.Data.Interfaces
{
    public interface ICategoriaRepository
    {
        Task AddAsync(Categoria newCategoria);
        Task<Categoria> GetAsync(string nome);
        Task<Categoria> GetAsync(long id);
        Task<int> DeleteAsync(Categoria dados);
        Task<IEnumerable<Categoria>> GetViewAllAsync(int skip, int take);
    }
}
