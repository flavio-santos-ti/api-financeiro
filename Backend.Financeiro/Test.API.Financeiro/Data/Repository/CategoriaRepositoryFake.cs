using API.Financeiro.Domain.Categoria;
using API.Financeiro.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Test.API.Financeiro.Data.Repository;

public class CategoriaRepositoryFake : ICategoriaRepository
{
    private IEnumerable<ViewCategoria> _categoria;

    public CategoriaRepositoryFake()
    {
        _categoria = new List<ViewCategoria>();
    }

    public async Task AddAsync(Categoria dados)
    {
        await Task.CompletedTask;
    }

    public async Task<Categoria> GetAsync(string nome)
    {
        await Task.Delay(1);

        if (nome == "Vendas")
        {
            return null;
        } else
        {
            Categoria categoria = new();
            categoria.Nome = "Despesas";
            categoria.Tipo = "S";

            return categoria;
        }
    }

    public async Task<Categoria> GetAsync(long id)
    {
        await Task.Delay(1);

        if (id == 1 )
        {
            Categoria categoria = new();
            categoria.Id = 1;
            categoria.Nome = "Despesas";
            categoria.Tipo = "S";

            return categoria;
        }
        else
        {
            return null;
        }
    }

    public async Task<int> DeleteAsync(Categoria dados)
    {
        await Task.Delay(1);
        return 1;
    }

    public async Task<IEnumerable<ViewCategoria>> GetViewAllAsync(int skip, int take)
    {
        await Task.Run(() => {

            _categoria = new List<ViewCategoria>() {
                new ViewCategoria(){ Id = 1, Nome = "Vendas", Tipo = "E"},
                new ViewCategoria(){ Id = 2, Nome = "Despesas", Tipo = "S"}
            };

        });

        return _categoria.Skip(skip).Take(take).ToList();

    }



}
