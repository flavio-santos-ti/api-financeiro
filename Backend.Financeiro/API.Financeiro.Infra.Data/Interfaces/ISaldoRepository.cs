using API.Financeiro.Domain.Saldo;
using System.Linq.Expressions;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface ISaldoRepository
{
    Task AddAsync(Saldo dados);
    Task<Saldo> GetAsync(Expression<Func<Saldo, bool>> condicao);
    Task<decimal> GetSaldoAsync(DateTime data);
    Task<DateTime> GetDataUltimoMovimentoAsync();
}
