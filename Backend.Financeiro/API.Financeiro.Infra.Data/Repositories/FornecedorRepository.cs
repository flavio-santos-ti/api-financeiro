using Api.Crud.Infra.Data.Context;
using Api.Crud.Infra.Data.Repositories.Base;
using API.Financeiro.Domain.Fornecedor;
using API.Financeiro.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Financeiro.Infra.Data.Repositories;

public class FornecedorRepository : RepositoryBase, IFornecedorRepository
{
    public FornecedorRepository(DatabaseContext context) : base(context) { }

    public async Task AddAsync(Fornecedor newFornecedor) => await base.AddAsync<Fornecedor>(newFornecedor);

    public async Task<Fornecedor> GetAsync(long id) => await base.GetAsync<Fornecedor>(b => b.Id == id);

    public async Task<Fornecedor> GetByPessoaIdAsync(long pessoaId) => await base.GetAsync<Fornecedor>(b => b.PessoaId == pessoaId);

    public async Task<int> DeleteAsync(Fornecedor dados) => await base.DeleteAsync(dados);

    public async Task<IEnumerable<ViewFornecedor>> GetViewAllAsync(int skip, int take)
    {
        DatabaseContext context = base.GetContext();

        var fornecedorView = await (
            from fornecedor in context.Fornecedores 
            join pessoa in context.Pessoas on fornecedor.PessoaId equals pessoa.Id
            select new ViewFornecedor
            {
                Id = fornecedor.Id,
                Nome = pessoa.Nome,
                PessoaId = pessoa.Id,
                DataInclusao = fornecedor.DataInclusao
            }).AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return fornecedorView;
    }
}
