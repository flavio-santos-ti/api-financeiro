using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Caixa;
using API.Financeiro.Domain.Exrato;
using API.Financeiro.Infra.Data.Interfaces;

namespace API.Financeiro.Business.Services;

public class ExtratoService : ServiceBase, IExtratoService
{
    private readonly IExtratoRepository _extratoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExtratoService(IExtratoRepository extratoRepository, IUnitOfWork unitOfWork)
    {
        _extratoRepository = extratoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> SetSaldoAnteriorAsync(AbrirCaixa dados, decimal saldoAnterior)
    {
        Extrato extrato = new();
        extrato.Descricao = "Saldo anterior";
        extrato.Tipo = "S";
        extrato.Valor = saldoAnterior;
        extrato.DataExtrato = dados.DataInformada;
        extrato.DataInclusao = DateTime.Now;

        await _extratoRepository.AddAsync(extrato);
        await _unitOfWork.SaveAsync();

        return extrato.Id;
    }

    public async Task<long> SetSaldoDiaAsync(FecharCaixa dados, decimal saldoAnterior)
    {
        Extrato extrato = new();
        extrato.Descricao = "Saldo do dia";
        extrato.Tipo = "S";
        extrato.Valor = saldoAnterior;
        extrato.DataExtrato = dados.DataInformada;
        extrato.DataInclusao = DateTime.Now;

        await _extratoRepository.AddAsync(extrato);
        await _unitOfWork.SaveAsync();

        return extrato.Id;
    }


    public async Task<Extrato> SetReceberAsync(ReceberCaixa dados, decimal saldoAnterior)
    {
        Extrato extrato = new();

        extrato.Descricao = dados.Descricao;
        extrato.Tipo = "C";
        extrato.DataExtrato = dados.Data;
        extrato.Valor = dados.Valor;
        extrato.Saldo = saldoAnterior + extrato.Valor;
        extrato.ValorRelatorio = extrato.Valor;
        extrato.DataInclusao = DateTime.Now;

        await _extratoRepository.AddAsync(extrato);
        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
        {
            return extrato;
        }
        else
        {
            return null;
        }
    }

    public async Task<Extrato> SetPagarAsync(PagarCaixa dados, decimal saldoAnterior)
    {
        Extrato extrato = new();

        extrato.Descricao = dados.Descricao;
        extrato.Tipo = "D";
        extrato.DataExtrato = dados.Data;
        extrato.Valor = dados.Valor * (-1);
        extrato.Saldo = saldoAnterior + extrato.Valor;
        extrato.ValorRelatorio = extrato.Valor;
        extrato.DataInclusao = DateTime.Now;

        await _extratoRepository.AddAsync(extrato);
        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
        {
            return extrato;
        }
        else
        {
            return null;
        }
    }


}
