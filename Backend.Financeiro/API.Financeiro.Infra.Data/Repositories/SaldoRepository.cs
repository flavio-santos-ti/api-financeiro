using Api.Crud.Infra.Data.Context;
using Api.Crud.Infra.Data.Repositories.Base;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Saldo;
using API.Financeiro.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Financeiro.Infra.Data.Repositories;

public class SaldoRepository : RepositoryBase, ISaldoRepository
{
    public SaldoRepository(DatabaseContext context) : base(context) { }

    public async Task AddAsync(Saldo dados) => await base.AddAsync<Saldo>(dados);

    public async Task<Saldo> GetAsync(Expression<Func<Saldo, bool>> condicao) => await base.GetAsync<Saldo>(condicao);

    public async Task<decimal> GetSaldoAsync(DateTime data)
    {
        DatabaseContext context = base.GetContext();

        var saldo = await context.Saldos
            .Where(b => b.DataSaldo <= data)
            .AsNoTracking()
            .OrderByDescending(b => b.Id)
            .FirstOrDefaultAsync();

        if (saldo == null) return 0;

        return saldo.Valor;
    }

}
