using Api.Crud.Infra.Data.Context;
using Api.Crud.Infra.Data.Repositories.Base;
using API.Financeiro.Domain.Pagar;
using API.Financeiro.Domain.Receber;
using API.Financeiro.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Financeiro.Infra.Data.Repositories;

public class PagarRepository : RepositoryBase, IPagarRepository
{
    public PagarRepository(DatabaseContext context) : base(context) { }

    public async Task AddAsync(Pagar newPagamento) => await base.AddAsync<Pagar>(newPagamento);

    public async Task<IEnumerable<ViewPagar>> GetViewAllAsync(int skip, int take)
    {
        DatabaseContext context = base.GetContext();

        var pagarView = await (
            from pagar in context.Pagamentos
            join fornecedor in context.Fornecedores on pagar.FornecedorId equals fornecedor.Id
            join pessoa in context.Pessoas on fornecedor.PessoaId equals pessoa.Id
            select new ViewPagar
            {
                Id = pagar.Id,
                FornecedorId = pagar.FornecedorId,
                Favorecido = pessoa.Nome,
                Descricao = pagar.Descricao,
                ValorReal = pagar.ValorReal,
                DataReal = pagar.DataReal,
                DataInclusao = pagar.DataInclusao
            }).AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return pagarView;
    }


}
