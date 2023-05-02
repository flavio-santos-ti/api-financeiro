using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Pagar;
using API.Financeiro.Domain.Receber;
using API.Financeiro.Domain.Result;
using API.Financeiro.Domain.Saldo;
using API.Financeiro.Infra.Data.Interfaces;
using API.Financeiro.Infra.Data.Repositories;
using AutoMapper;

namespace API.Financeiro.Business.Services;

public class PagarService : ServiceBase, IPagarService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPagarRepository _pagarRepository;
    private readonly ICaixaService _caixaService;
    private readonly IFornecedorService _fornecedorService;
    private readonly ICategoriaService _categoriaService;
    private readonly IMapper _mapper;

    public PagarService(IUnitOfWork unitOfWork, IPagarRepository pagarRepository, ICaixaService caixaService, IFornecedorService fornecedorService, ICategoriaService categoriaService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _pagarRepository = pagarRepository;
        _caixaService = caixaService;
        _fornecedorService = fornecedorService;
        _categoriaService = categoriaService;
        _mapper = mapper;
    }

    private async Task<ServiceResult> ParseAsync(CreatePagar dados)
    {
        bool isCaixaFechado = await _caixaService.IsFechadoAsync(dados.DataReal);

        if (isCaixaFechado)
        {
            return base.Error("Caixa fechado", "Pagamento de título");
        }

        bool isClienteValid = await _fornecedorService.IsValidAsync(dados.FornecedorId);

        if (!isClienteValid)
        {
            return base.Error("Cliente inválido", "Pagamento de título");
        }

        bool isCategoriaValid = await _categoriaService.IsValidAsync(dados.CategoriaId);

        if (!isCategoriaValid)
        {
            return base.Error("Categoria inválida", "Pagamento de título");
        }

        return base.Successed("Ok", "Validação");
    }

    public async Task<ServiceResult> CreateAsync(CreatePagar dados)
    {
        dados.Descricao = base.ClearString(dados.Descricao.ToUpper());
        dados.DataReal = dados.DataReal.Date;

        await _unitOfWork.BeginTransactionAsync();

        var situacao = await this.ParseAsync(dados);

        if (!situacao.Successed)
        {
            return situacao;
        }

        decimal saldoAnterior = await _caixaService.GetSaldoAsync(dados.DataReal);

        Extrato newExtrato = new();
        newExtrato.Tipo = "D";
        newExtrato.Descricao = dados.Descricao;
        newExtrato.DataExtrato = dados.DataReal;
        newExtrato.Valor = dados.ValorReal * -1;
        newExtrato.Saldo = saldoAnterior + newExtrato.Valor;
        newExtrato.ValorRelatorio = newExtrato.Valor * -1;
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
        saldoMovimento.Valor = saldoAnterior + (dados.ValorReal * -1);
        saldoMovimento.ExtratoId = newExtrato.Id;

        var saldo = await _caixaService.RegistraSaldoAsync(saldoMovimento);

        if (!saldo.Successed)
        {
            await _unitOfWork.RolbackAsync();
            return saldo;
        }

        var titulo = _mapper.Map<Pagar>(dados);
        titulo.DataInclusao = DateTime.Now;
        titulo.ExtratoId = extratoId;

        await _pagarRepository.AddAsync(titulo);
        
        await _unitOfWork.SaveAsync();
        await _unitOfWork.CommitAsync();

        return base.SuccessedAdd(titulo, "Pagamento de Título");
    }

}
