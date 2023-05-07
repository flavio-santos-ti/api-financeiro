using API.Financeiro.Domain.Caixa;
using API.Financeiro.Domain.Exrato;

namespace API.Financeiro.Business.Interfaces;

public interface IExtratoService
{
    Task<long> SetSaldoAnteriorAsync(AbrirCaixa dados, decimal saldoAnterior);
    Task<long> SetSaldoDiaAsync(FecharCaixa dados, decimal saldoAnterior);
    Task<Extrato> SetReceberAsync(ReceberCaixa dados, decimal saldoAnterior);
    Task<Extrato> SetPagarAsync(PagarCaixa dados, decimal saldoAnterior);
}
