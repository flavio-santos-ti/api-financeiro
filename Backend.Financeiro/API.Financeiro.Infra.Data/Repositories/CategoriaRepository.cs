using Api.Crud.Infra.Data.Context;
using Api.Crud.Infra.Data.Repositories.Base;
using Api.Crud.Infra.Data.UnitOfWork;
using API.Financeiro.Domain.Categoria;
using API.Financeiro.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Financeiro.Infra.Data.Repositories;

public class CategoriaRepository : RepositoryBase, ICategoriaRepository
{
    public CategoriaRepository(DatabaseContext context) : base(context) { }

    public async Task AddAsync(Categoria newCategoria) => await base.AddAsync<Categoria>(newCategoria);

    public async Task<Categoria> GetAsync(string nome) => await base.GetAsync<Categoria>(b => b.Nome == nome);

    public async Task<Categoria> GetAsync(long id) => await base.GetAsync<Categoria>(b => b.Id == id);

    public async Task<int> DeleteAsync(Categoria dados) => await base.DeleteAsync(dados); 

    public async Task<IEnumerable<Categoria>> GetViewAllAsync(int skip, int take)
    {
        DatabaseContext context = base.GetContext();

        var categoriaView = await(
            from categoria in context.Categorias
            select new Categoria
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Tipo = categoria.Tipo
            }).AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        


        return categoriaView;
    }

}
