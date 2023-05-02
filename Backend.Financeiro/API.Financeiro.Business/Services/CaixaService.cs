using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Extrato;
using API.Financeiro.Domain.Result;
using API.Financeiro.Domain.Saldo;
using API.Financeiro.Infra.Data.Interfaces;
using API.Financeiro.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Services;

public class CaixaService : ServiceBase, ICaixaService
{
    private readonly ISaldoRepository _saldoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExtratoRepository _extratoRepository;

    public CaixaService(ISaldoRepository saldoRepository, IUnitOfWork unitOfWork, IExtratoRepository extratoRepository)
    {
        _saldoRepository = saldoRepository;
        _unitOfWork = unitOfWork;
        _extratoRepository = extratoRepository;
    }

    public async Task<bool> IsFechadoAsync(DateTime data)
    {
        var saldoFinalExistente = await _saldoRepository.GetAsync(b => b.DataSaldo <= data && b.Tipo == "F");

        return (saldoFinalExistente != null);
    }

    public async Task<ServiceResult> RegistraEntradaAsync(Extrato dados)
    {
        await _extratoRepository.AddAsync(dados);
        await _unitOfWork.SaveAsync();

        return base.SuccessedAdd(dados, "Extrato");
    }

    public async Task<decimal> GetSaldoAsync(DateTime data)
    {
        return await _saldoRepository.GetSaldoAsync(data);
    }

    public async Task<ServiceResult> RegistraSaldoAsync(Saldo dados)
    {
        await _saldoRepository.AddAsync(dados);
        await _unitOfWork.SaveAsync();

        return base.SuccessedAdd(dados, "Saldo");
    }

    public async Task<ServiceResult> GetListarAsync(PeriodoExtrato filtro)
    {
        var extrato = await _extratoRepository.GetListarAsync(filtro);

        return base.SuccessedViewAll(extrato, "Extrato", extrato.Count());
    }

}
