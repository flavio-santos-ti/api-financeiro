using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Caixa;
using API.Financeiro.Domain.Saldo;
using API.Financeiro.Infra.Data.Interfaces;

namespace API.Financeiro.Business.Services;

public class SaldoService : ServiceBase, ISaldoService
{
    private readonly ISaldoRepository _saldoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaldoService(ISaldoRepository saldoRepository, IUnitOfWork unitOfWork)
    {
        _saldoRepository = saldoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> IsAbertoAsync(DateTime data)
    {
        DateTime somenteData = data.Date;
        var caixaFechado = await _saldoRepository.GetAsync(b => b.DataSaldo == somenteData && b.Tipo == "I");

        return (caixaFechado != null);
    }

    public async Task<bool> IsFechadoAsync(DateTime data)
    {
        var caixaFechado = await _saldoRepository.GetAsync(b => b.DataSaldo == data.Date && b.Tipo == "F");
        return (caixaFechado != null);
    }

    public async Task<DateTime> GetDataUltimoMovimentoAsync() => await _saldoRepository.GetDataUltimoMovimentoAsync();

    public async Task<decimal> GetSaldoAsync(DateTime data) => await _saldoRepository.GetSaldoAsync(data);

    public async Task<ViewSaldo> SetSaldoInicial(AbrirCaixa dados, decimal saldoAnterior, long extratoId)
    {
        Saldo saldo = new();
        saldo.Tipo = "I";
        saldo.DataSaldo = dados.DataInformada.Date;
        saldo.Valor = saldoAnterior;
        saldo.ExtratoId = extratoId;
        saldo.DataInclusao = DateTime.Now;
        
        await _saldoRepository.AddAsync(saldo);
        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
        {
            ViewSaldo newSaldo = new();
            newSaldo.Id = saldo.Id;
            newSaldo.Valor = saldo.Valor;
            return newSaldo;
        } 
        else
        {
            return null;
        }
    }

    public async Task<ViewSaldo> SetSaldoFinal(FecharCaixa dados, decimal saldoAnterior, long extratoId)
    {
        Saldo saldo = new();
        saldo.Tipo = "F";
        saldo.DataSaldo = dados.DataInformada.Date;
        saldo.Valor = saldoAnterior;
        saldo.ExtratoId = extratoId;
        saldo.DataInclusao = DateTime.Now;

        await _saldoRepository.AddAsync(saldo);
        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
        {
            ViewSaldo newSaldo = new();
            newSaldo.Id = saldo.Id;
            newSaldo.Valor = saldo.Valor;
            return newSaldo;
        }
        else
        {
            return null;
        }
    }


    public async Task<Saldo> SetReceberAsync(ReceberCaixa dados, decimal saldoAnterior, long extratoId)
    {
        Saldo saldo = await _saldoRepository.GetAsync(b => b.DataSaldo == dados.Data.Date && b.Tipo == "M");

        if (saldo == null)
        {
            Saldo newSaldo = new();
            newSaldo.Tipo = "M";
            newSaldo.DataSaldo = dados.Data.Date;
            newSaldo.Valor = saldoAnterior + dados.Valor;
            newSaldo.ExtratoId = extratoId;
            newSaldo.DataInclusao = DateTime.Now;

            await _saldoRepository.AddAsync(newSaldo);
            int result = await _unitOfWork.SaveAsync();
            
            return newSaldo;
        }
        else
        {
            saldo.Valor = saldoAnterior + dados.Valor;
            saldo.ExtratoId = extratoId;
            saldo.DataInclusao = DateTime.Now;
            int result = await _unitOfWork.SaveAsync();
            return saldo;
        }
    }

    public async Task<Saldo> SetPagarAsync(PagarCaixa dados, decimal saldoAnterior, long extratoId)
    {
        Saldo saldo = await _saldoRepository.GetAsync(b => b.DataSaldo == dados.Data.Date && b.Tipo == "M");

        if (saldo == null)
        {
            Saldo newSaldo = new();
            newSaldo.Tipo = "M";
            newSaldo.DataSaldo = dados.Data.Date;
            newSaldo.Valor = saldoAnterior + ( dados.Valor *(-1));
            newSaldo.ExtratoId = extratoId;
            newSaldo.DataInclusao = DateTime.Now;

            await _saldoRepository.AddAsync(newSaldo);
            await _unitOfWork.SaveAsync();

            return newSaldo;
        }
        else
        {
            saldo.Valor = saldoAnterior + (dados.Valor * (-1));
            saldo.ExtratoId = extratoId;
            saldo.DataInclusao = DateTime.Now;

            await _unitOfWork.SaveAsync();

            return saldo;
        }
    }


}
