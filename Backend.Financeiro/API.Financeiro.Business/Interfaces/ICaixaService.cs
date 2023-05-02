using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Extrato;
using API.Financeiro.Domain.Result;
using API.Financeiro.Domain.Saldo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Interfaces;

public interface ICaixaService
{
    Task<bool> IsFechadoAsync(DateTime data);
    Task<ServiceResult> RegistraEntradaAsync(Extrato dados);
    Task<decimal> GetSaldoAsync(DateTime data);
    Task<ServiceResult> RegistraSaldoAsync(Saldo dados);
    Task<ServiceResult> GetListarAsync(PeriodoExtrato filtro);
}
