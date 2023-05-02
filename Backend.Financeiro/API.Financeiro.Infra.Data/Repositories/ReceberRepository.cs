using Api.Crud.Infra.Data.Context;
using Api.Crud.Infra.Data.Repositories.Base;
using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Receber;
using API.Financeiro.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Financeiro.Infra.Data.Repositories;

public class ReceberRepository : RepositoryBase, IReceberRepository
{
    public ReceberRepository(DatabaseContext context) : base(context) { }

    public async Task AddAsync(Receber newRecebimento) => await base.AddAsync<Receber>(newRecebimento);

    public async Task<IEnumerable<ViewReceber>> GetViewAllAsync(int skip, int take)
    {
        DatabaseContext context = base.GetContext();

        var receberView = await (
            from receber in context.Recebimentos
            join cliente in context.Clientes on receber.ClienteId equals cliente.Id
            join pessoa in context.Pessoas on cliente.PessoaId equals pessoa.Id
            select new ViewReceber
            {
                Id = receber.Id,
                ClienteId = receber.ClienteId,
                Origem = pessoa.Nome,
                Descricao = receber.Descricao,
                ValorReal = receber.ValorReal,
                DataReal = receber.DataReal,
                DataInclusao = receber.DataInclusao
            }).AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return receberView;
    }


}
