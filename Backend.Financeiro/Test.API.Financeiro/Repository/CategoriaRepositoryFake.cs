using API.Financeiro.Domain.Categoria;
using API.Financeiro.Infra.Data.Interfaces;

namespace Test.API.Financeiro.Repository;

public class CategoriaRepositoryFake : ICategoriaRepository
{
    private IEnumerable<Categoria> _categoria;

    public CategoriaRepositoryFake()
    {
        _categoria = new List<Categoria>();
    }

    public async Task AddAsync(Categoria newCategoria)
    {
        await Task.CompletedTask;
    }

    public async Task<Categoria> GetAsync(string nome)
    {
        await Task.Run(() =>
        {

            _categoria = new List<Categoria>() {
                new Categoria(){ Id = 1, Nome = "Entradas", Tipo = "E"},
                new Categoria(){ Id = 2, Nome = "Saídas", Tipo = "S"}
            };

        });

        return _categoria.Where(b => b.Nome == nome).FirstOrDefault();

    }

    public async Task<Categoria> GetAsync(long id)
    {
        await Task.Delay(1);

        await Task.Run(() =>
        {

            _categoria = new List<Categoria>() {
                new Categoria(){ Id = 1, Nome = "Entradas", Tipo = "E"},
                new Categoria(){ Id = 2, Nome = "Saídas", Tipo = "S"}
            };

        });

        return _categoria.Where(b => b.Id == id).FirstOrDefault();
    }

    public async Task<int> DeleteAsync(Categoria dados)
    {
        await Task.Delay(1);
        return 1;
    }

    public async Task<IEnumerable<Categoria>> GetViewAllAsync(int skip, int take)
    {
        await Task.Run(() =>
        {

            _categoria = new List<Categoria>() {
                new Categoria(){ Id = 1, Nome = "Entradas", Tipo = "E"},
                new Categoria(){ Id = 2, Nome = "Saídas", Tipo = "S"}
            };

        });

        return _categoria.Skip(skip).Take(take).ToList();
    }

}
