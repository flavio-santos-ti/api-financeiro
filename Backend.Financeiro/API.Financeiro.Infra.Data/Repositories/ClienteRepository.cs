using Api.Crud.Infra.Data.Context;
using Api.Crud.Infra.Data.Repositories.Base;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Financeiro.Infra.Data.Repositories;

public class ClienteRepository : RepositoryBase, IClienteRepository
{
    public ClienteRepository(DatabaseContext context) : base(context) { }

    public async Task AddAsync(Cliente newCliente) => await base.AddAsync<Cliente>(newCliente);

    public async Task<Cliente> GetAsync(long id) => await base.GetAsync<Cliente>(b => b.Id == id);

    public async Task<Cliente> GetByPessoaIdAsync(long pessoaId) => await base.GetAsync<Cliente>(b => b.PessoaId == pessoaId);

    public async Task<int> DeleteAsync(Cliente dados) => await base.DeleteAsync(dados);

    public async Task<IEnumerable<ViewCliente>> GetViewAllAsync(int skip, int take)
    {
        DatabaseContext context = base.GetContext();

        var clienteView = await (
            from cliente in context.Clientes
            join pessoa in context.Pessoas on cliente.PessoaId equals pessoa.Id
            select new ViewCliente
            {
                Id = cliente.Id,
                Nome = pessoa.Nome,
                PessoaId = pessoa.Id,
                DataInclusao = cliente.DataInclusao
            }).AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return clienteView;
    }
}
