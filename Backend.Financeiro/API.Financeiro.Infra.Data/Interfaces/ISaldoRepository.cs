using API.Financeiro.Domain.Saldo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface ISaldoRepository
{
    Task AddAsync(Saldo dados);
    Task<Saldo> GetAsync(Expression<Func<Saldo, bool>> condicao);
    Task<decimal> GetSaldoAsync(DateTime data);
    Task<DateTime> GetDataUltimoMovimentoAsync();
}
