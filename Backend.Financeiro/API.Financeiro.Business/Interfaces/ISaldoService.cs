using API.Financeiro.Domain.Caixa;
using API.Financeiro.Domain.Saldo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Interfaces;

public interface ISaldoService
{
    Task<bool> IsAbertoAsync(DateTime data);
    Task<bool> IsFechadoAsync(DateTime data);
    Task<DateTime> GetDataUltimoMovimentoAsync();
    Task<decimal> GetSaldoAsync(DateTime data);
    Task<ViewSaldo> SetSaldoInicial(AbrirCaixa dados, decimal saldoAnterior, long extratoId);
    Task<ViewSaldo> SetSaldoFinal(FecharCaixa dados, decimal saldoAnterior, long extratoId);
    Task<Saldo> SetReceberAsync(ReceberCaixa dados, decimal saldoAnterior, long extratoId);
    Task<Saldo> SetPagarAsync(PagarCaixa dados, decimal saldoAnterior, long extratoId);
}
