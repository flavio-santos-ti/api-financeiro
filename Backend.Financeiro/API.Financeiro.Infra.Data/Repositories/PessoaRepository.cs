using Api.Crud.Infra.Data.Context;
using Api.Crud.Infra.Data.Repositories.Base;
using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Pessoa;
using API.Financeiro.Infra.Data.Interfaces;

namespace API.Financeiro.Infra.Data.Repositories;

public class PessoaRepository : RepositoryBase, IPessoaRepository
{
    public PessoaRepository(DatabaseContext context) : base(context) { }

    public async Task AddAsync(Pessoa newPessoa) => await base.AddAsync<Pessoa>(newPessoa);
    public async Task<Pessoa> GetAsync(string hashNome) => await base.GetAsync<Pessoa>(b => b.HashNome == hashNome);
}
