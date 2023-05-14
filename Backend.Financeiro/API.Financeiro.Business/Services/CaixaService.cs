using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Caixa;
using API.Financeiro.Domain.Result;

namespace API.Financeiro.Business.Services;

public class CaixaService : ServiceBase, ICaixaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExtratoService _extratoService;
    private readonly ISaldoService _saldoService;
    private readonly ICategoriaService _categoriaService;
    private readonly IClienteService _clienteService;
    private readonly IFornecedorService _fornecedorService;

    public CaixaService(IUnitOfWork unitOfWork, IExtratoService extratoService, ISaldoService saldoService, ICategoriaService categoriaService, IClienteService clienteService, IFornecedorService fornecedorService)
    {
        _unitOfWork = unitOfWork;
        _extratoService = extratoService;
        _saldoService = saldoService;
        _categoriaService = categoriaService;
        _clienteService = clienteService;
        _fornecedorService = fornecedorService;
    }

    private async Task<ServiceResult> ParseAberturaAsync(AbrirCaixa dados, DateTime dataUltimoSaldo)
    {
        if (await _saldoService.IsFechadoAsync(dados.DataInformada.Date))
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Encontra-se fechado.", "Abertura de Caixa");
        }

        if (await _saldoService.IsAbertoAsync(dados.DataInformada.Date))
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Já encontra-se aberto.", "Abertura de Caixa");
        }

        if (dataUltimoSaldo.ToString("dd/MM/yyyy") == dados.DataInformada.ToString("dd/MM/yyyy"))
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("A data informada não pode ser igual a data do último saldo.", "Abertura de Caixa");
        }

        return base.Successed("Ok", "Abertura de Caixa");
    }

    private async Task<ServiceResult> ParseFechamentoAsync(FecharCaixa dados, DateTime dataUltimoSaldo)
    {
        if (await _saldoService.IsFechadoAsync(dados.DataInformada.Date))
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Encontra-se fechado.", "Abertura de Caixa");
        }

        if (!await _saldoService.IsAbertoAsync(dados.DataInformada.Date))
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Não foi encontrado caixa aberto para fechamento.", "Fechamento de Caixa");
        }


        if (dataUltimoSaldo.ToString("dd/MM/yyyy") != dados.DataInformada.ToString("dd/MM/yyyy"))
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("A data informada não pode ser diferente a data do último saldo.", "Fechamento de Caixa");
        }

        return base.Successed("Ok", "Fechamento de Caixa");
    }

    private async Task<ServiceResult> ParseRecebimentoAsync(ReceberCaixa dados)
    {
        var assunto = "Registro de Recebimento";
        
        var categoria = await _categoriaService.GetAsync(dados.CategoriaId);

        if (categoria == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.ErrorNaoEncontrado(assunto);
        }

        if (categoria.Tipo != "E")
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Categoria inválida.", assunto);
        }

        var cliente = await _clienteService.GetAsync(dados.ClienteId);

        if (cliente == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.ErrorNaoEncontrado(assunto);
        }

        return base.Successed("Ok", assunto);
    }

    private async Task<ServiceResult> ParsePagamentoAsync(PagarCaixa dados)
    {
        var assunto = "Registro de Pagamento";

        var categoria = await _categoriaService.GetAsync(dados.CategoriaId);

        if (categoria == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.ErrorNaoEncontrado(assunto);
        }

        if (categoria.Tipo != "S")
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Categoria inválida.", assunto);
        }

        var fornecedor = await _fornecedorService.GetAsync(dados.FornecedorId);

        if (fornecedor == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.ErrorNaoEncontrado(assunto);
        }

        return base.Successed("Ok", assunto);
    }


    private async Task<ServiceResult> IsAbertoAsync(DateTime dataMovimento)
    {
        if (!await _saldoService.IsAbertoAsync(dataMovimento.Date))
        {
            await _unitOfWork.RolbackAsync();
            return base.Error($"O movimento do dia {dataMovimento.ToString("dd-MM-yyyy")} ainda não foi aberto.", "Movimentação de Caixa");
        }

        if (await _saldoService.IsFechadoAsync(dataMovimento.Date))
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Encontra-se fechado.", "Caixa");
        }

        return base.Successed("Pronto para uso.", "Caixa");
    }


    public async Task<ServiceResult> AbrirAsync(AbrirCaixa dados)
    {
        await _unitOfWork.BeginTransactionAsync();

        // Valida Abertura
        decimal saldoAnterior = 0;
        DateTime dataUltimoSaldo = await _saldoService.GetDataUltimoMovimentoAsync();

        var parseAbertura = await ParseAberturaAsync(dados, dataUltimoSaldo.Date);;

        if (!parseAbertura.Successed) return parseAbertura;

        // Registra Extrato
        if (dataUltimoSaldo.Date > new DateTime(0001, 1, 1))
        {
            saldoAnterior = await _saldoService.GetSaldoAsync(dataUltimoSaldo.Date);
        }

        long extratoId = await _extratoService.SetSaldoAnteriorAsync(dados, saldoAnterior); 
            
        if (extratoId == 0)
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Erro ao tentar registrar o extrato.", "Abertura de Caixa");
        }

        // Registra Saldo
        var saldo = await _saldoService.SetSaldoInicial(dados, saldoAnterior, extratoId);
        
        if (saldo == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Erro ao tentar registrar o saldo.", "Abertura de Caixa");
        }
                
        await _unitOfWork.CommitAsync();

        return base.Successed("Aberto com sucesso.", "Caixa", saldo, saldo.Id);
    }


    public async Task<ServiceResult> SetFecharAsync(FecharCaixa dados)
    {
        await _unitOfWork.BeginTransactionAsync();

        // Valida Fechamento
        decimal saldoAnterior = 0;
        DateTime dataUltimoSaldo = await _saldoService.GetDataUltimoMovimentoAsync();

        var parseFechamento = await ParseFechamentoAsync(dados, dataUltimoSaldo.Date); ;

        if (!parseFechamento.Successed) return parseFechamento;

        // Registra Extrato
        if (dataUltimoSaldo.Date > new DateTime(0001, 1, 1))
        {
            saldoAnterior = await _saldoService.GetSaldoAsync(dataUltimoSaldo.Date);
        }

        long extratoId = await _extratoService.SetSaldoDiaAsync(dados, saldoAnterior);

        if (extratoId == 0)
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Erro ao tentar registrar o extrato.", "Fechamento de Caixa");
        }

        // Registra Saldo
        var saldo = await _saldoService.SetSaldoFinal(dados, saldoAnterior, extratoId);

        if (saldo == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Erro ao tentar registrar o saldo.", "Fechamento de Caixa");
        }

        await _unitOfWork.CommitAsync();

        return base.Successed("Fechado com sucesso.", "Caixa", saldo, saldo.Id);

    }

    public async Task<ServiceResult> SetReceberAsync(ReceberCaixa dados)
    {
        await _unitOfWork.BeginTransactionAsync();

        var parseRecebimento = await ParseRecebimentoAsync(dados);

        if (!parseRecebimento.Successed) return parseRecebimento;
        
        // Valida Status do Caixa
        var caixaAberto = await IsAbertoAsync(dados.Data.Date);

        if (!caixaAberto.Successed)
        {
            await _unitOfWork.RolbackAsync();
            return caixaAberto;
        }

        // Recupera Saldo
        decimal saldoAnterior = 0;
        DateTime dataUltimoSaldo = await _saldoService.GetDataUltimoMovimentoAsync();

        if (dataUltimoSaldo > new DateTime(0001, 1, 1))
        {
            saldoAnterior = await _saldoService.GetSaldoAsync(dataUltimoSaldo.Date);
        }

        // Registra Extrato
        var cliente = await _clienteService.GetAsync(dados.ClienteId);
        dados.ClienteId = cliente.PessoaId;
        var extrato = await _extratoService.SetReceberAsync(dados, saldoAnterior);

        if (extrato == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Erro ao tentar registrar o extrato.", "Recebimento no Caixa");
        }

        // Registra Extrato
        var resultSaldo = await _saldoService.SetReceberAsync(dados, saldoAnterior, extrato.Id);

        if (resultSaldo == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Erro ao tentar registrar o saldo.", "Recebimento no Caixa");
        }

        var result = new { SaldoId = resultSaldo.Id, ValorSaldo = resultSaldo.Valor, ExtratoId = extrato.Id, ValorExtrato = extrato.Valor };

        await _unitOfWork.CommitAsync();
        return base.Successed("Recebimento registrado com sucesso.", "Caixa", result, extrato.Id);

    }

    public async Task<ServiceResult> SetPagarAsync(PagarCaixa dados)
    {
        await _unitOfWork.BeginTransactionAsync();

        var parsePagamento = await ParsePagamentoAsync(dados);

        if (!parsePagamento.Successed) return parsePagamento;

        // Valida Status do Caixa
        var caixaAberto = await IsAbertoAsync(dados.Data.Date);

        if (!caixaAberto.Successed)
        {
            await _unitOfWork.RolbackAsync();
            return caixaAberto;
        }

        // Recupera Saldo
        decimal saldoAnterior = 0;
        DateTime dataUltimoSaldo = await _saldoService.GetDataUltimoMovimentoAsync();

        if (dataUltimoSaldo > new DateTime(0001, 1, 1))
        {
            saldoAnterior = await _saldoService.GetSaldoAsync(dataUltimoSaldo.Date);
        }

        // Registra Extrato
        var fornecedor = await _fornecedorService.GetAsync(dados.FornecedorId);
        dados.FornecedorId = fornecedor.PessoaId;
        var extrato = await _extratoService.SetPagarAsync(dados, saldoAnterior);

        if (extrato == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Erro ao tentar registrar o extrato.", "Pagamento no Caixa");
        }

        // Registra Extrato
        var resultSaldo = await _saldoService.SetPagarAsync(dados, saldoAnterior, extrato.Id);

        if (resultSaldo == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.Error("Erro ao tentar registrar o saldo.", "Pagamento no Caixa");
        }

        var result = new { SaldoId = resultSaldo.Id, ValorSaldo = resultSaldo.Valor, ExtratoId = extrato.Id, ValorExtrato = extrato.Valor };

        await _unitOfWork.CommitAsync();
        return base.Successed("Pagamento registrado com sucesso.", "Pagamento Caixa", result, extrato.Id);
    }
}
