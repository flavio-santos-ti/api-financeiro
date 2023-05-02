using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Receber;
using API.Financeiro.Domain.Result;
using API.Financeiro.Domain.Saldo;
using API.Financeiro.Infra.Data.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Financeiro.Business.Services;

public class ReceberService : ServiceBase, IReceberService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReceberRepository _receberRepository;
    private readonly ICaixaService _caixaService;
    private readonly IClienteService _clienteService;
    private readonly ICategoriaService _categoriaService;
    private readonly IMapper _mapper;

    public ReceberService(IUnitOfWork unitOfWork, IReceberRepository receberRepository, ICaixaService caixaService, IClienteService clienteService, ICategoriaService categoriaService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _receberRepository = receberRepository;
        _caixaService = caixaService;
        _clienteService = clienteService;
        _categoriaService = categoriaService;
        _mapper = mapper;
    }

    private async Task<ServiceResult> ParseAsync(CreateReceber dados)
    {

        bool isCaixaFechado = await _caixaService.IsFechadoAsync(dados.DataReal);

        if (isCaixaFechado)
        {
            return base.Error("Caixa fechado", "Recebimento de título");
        }

        bool isClienteValid = await _clienteService.IsValidAsync(dados.ClienteId);

        if (!isClienteValid)
        {
            return base.Error("Cliente inválido", "Recebimento de título");
        }

        bool isCategoriaValid = await _categoriaService.IsValidAsync(dados.CategoriaId);

        if (!isCategoriaValid)
        {
            return base.Error("Categoria inválida", "Recebimento de título");
        }

        return base.Successed("Ok", "Validação");
    }


    public async Task<ServiceResult> CreateAsync(CreateReceber dados)
    {
        dados.Descricao = base.ClearString(dados.Descricao.ToUpper());
        dados.DataReal = dados.DataReal.Date;

        await _unitOfWork.BeginTransactionAsync();

        var situacao = await this.ParseAsync(dados);

        if (!situacao.Successed)
        {
            await _unitOfWork.RolbackAsync();
            return situacao;
        }

        decimal saldoAnterior = await _caixaService.GetSaldoAsync(dados.DataReal);

        Extrato newExtrato= new();
        newExtrato.Tipo = "C";
        newExtrato.Descricao = dados.Descricao;
        newExtrato.DataExtrato = dados.DataReal;
        newExtrato.Valor = dados.ValorReal;
        newExtrato.Saldo = saldoAnterior + newExtrato.Valor;
        newExtrato.ValorRelatorio = newExtrato.Valor;
        newExtrato.DataInclusao = DateTime.Now;

        var extrato = await _caixaService.RegistraEntradaAsync(newExtrato);

        if (!extrato.Successed)
        {
            await _unitOfWork.RolbackAsync();
            return extrato;
        }

        long extratoId = newExtrato.Id;

        Saldo saldoMovimento = new();
        saldoMovimento.Tipo = "M";
        saldoMovimento.DataSaldo = dados.DataReal;
        saldoMovimento.Valor = saldoAnterior + dados.ValorReal;
        saldoMovimento.ExtratoId = newExtrato.Id;
        
        var saldo =  await _caixaService.RegistraSaldoAsync(saldoMovimento);

        if (!saldo.Successed)
        {
            await _unitOfWork.RolbackAsync();
            return saldo;
        }

        Receber titulo = _mapper.Map<Receber>(dados);
        titulo.DataInclusao = DateTime.Now;
        titulo.ExtratoId = extratoId;

        await _receberRepository.AddAsync(titulo);
        await _unitOfWork.SaveAsync();

        await _unitOfWork.CommitAsync();

        return base.SuccessedAdd(titulo, "Recebimento de Título");
    }
}
