using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Services;
using API.Financeiro.Domain.Caixa;
using API.Financeiro.Domain.Saldo;
using API.Financeiro.Infra.Data.Interfaces;
using Test.API.Financeiro.Repository;
using Test.API.Financeiro.UnitOfWork;

namespace Test.API.Financeiro.Services;

[TestClass]
public class SaldoServiceTest
{
    private readonly ISaldoRepository _saldoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaldoServiceTest()
    {
        _unitOfWork = new UnitOfWorkFake();
        DateTime DataInformada = new DateTime(2023, 1, 1);
        _saldoRepository = new SaldoRepositoryFake(DataInformada);
    }

    [TestMethod]
    [TestCategory("Método - IsAbertoAsync()")]
    public async Task IsAbertoAsync_retorna_True()
    {
        // Arrange
        DateTime dataTransacao = new DateTime(2023, 1, 2);

        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);

        // Act
        bool retorno = await saldoService.IsAbertoAsync(dataTransacao);

        bool resultado = retorno;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - IsFechadoAsync()")]
    public async Task IsFechadoAsync_retorna_True()
    {
        // Arrange
        DateTime dataTransacao = new DateTime(2023, 1, 1);

        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);

        // Act
        bool retorno = await saldoService.IsFechadoAsync(dataTransacao);

        bool resultado = retorno;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - GetDataUltimoMovimentoAsync()")]
    public async Task GetDataUltimoMovimentoAsync_retorna_True()
    {
        // Arrange
        DateTime dataTransacao = new DateTime(2023, 1, 1);
        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);

        // Act
        DateTime retorno = await saldoService.GetDataUltimoMovimentoAsync();

        bool resultado = retorno == dataTransacao;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - SetSaldoInicial()")]
    public async Task SetSaldoInicial_retorna_True()
    {
        // Arrange
        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);
        decimal saldoAnterior = 50;
        AbrirCaixa dados = new();
        dados.DataInformada = new DateTime(2023, 1, 1);

        // Act
        ViewSaldo retorno = await saldoService.SetSaldoInicial(dados, saldoAnterior, 1);

        bool resultado = retorno != null;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - SetSaldoFinal()")]
    public async Task SetSaldoFinal_retorna_True()
    {
        // Arrange
        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);
        decimal saldoAnterior = 50;
        FecharCaixa dados = new();
        dados.DataInformada = new DateTime(2023, 1, 1);

        // Act
        ViewSaldo retorno = await saldoService.SetSaldoFinal(dados, saldoAnterior, 1);

        bool resultado = retorno != null;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - SetReceberAsync()")]
    public async Task SetReceberAsync_retorna_True()
    {
        // Arrange
        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);
        decimal saldoAnterior = 50;
        ReceberCaixa dados = new();
        dados.CategoriaId = 1;
        dados.ClienteId = 1;
        dados.Descricao = "Aporte de Capital";
        dados.Valor = 1500;
        dados.Data = new DateTime(2023, 1, 1);

        // Act
        Saldo retorno = await saldoService.SetReceberAsync(dados, saldoAnterior, 1);

        bool resultado = retorno != null;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - SetPagarAsync()")]
    public async Task SetPagarAsync_retorna_True()
    {
        // Arrange
        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);
        decimal saldoAnterior = 50;
        PagarCaixa dados = new();
        dados.CategoriaId = 2;
        dados.FornecedorId = 2;
        dados.Descricao = "Serviços de Motoboy";
        dados.Valor = 100;
        dados.Data = new DateTime(2023, 1, 2);

        // Act
        Saldo retorno = await saldoService.SetPagarAsync(dados, saldoAnterior, 1);

        bool resultado = retorno != null;

        // Assert
        Assert.IsTrue(resultado);
    }

}
