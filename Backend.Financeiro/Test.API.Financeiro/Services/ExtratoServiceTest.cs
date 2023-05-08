using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Services;
using API.Financeiro.Domain.Caixa;
using API.Financeiro.Domain.Exrato;
using API.Financeiro.Infra.Data.Interfaces;
using Test.API.Financeiro.Repository;
using Test.API.Financeiro.UnitOfWork;

namespace Test.API.Financeiro.Services;

[TestClass]
public class ExtratoServiceTest
{
    private readonly IExtratoRepository _extratoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExtratoServiceTest()
    {
        _extratoRepository = new ExtratoRepositoryFake();
        _unitOfWork = new UnitOfWorkFake();
    }

    [TestMethod]
    [TestCategory("Método - SetSaldoAnteriorAsync()")]
    public async Task SetSaldoAnteriorAsync_retorna_true()
    {
        // Arrange
        AbrirCaixa dados = new();
        dados.DataInformada = new DateTime(2023, 1, 1);
        decimal saldoAnterior = 2500;

        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);

        // Act
        long retorno = await extratoService.SetSaldoAnteriorAsync(dados, saldoAnterior);
        
        bool resultado = retorno > 0;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - SetSaldoDiaAsync()")]
    public async Task SetSaldoDiaAsync_retorna_true()
    {
        // Arrange
        FecharCaixa dados = new();
        dados.DataInformada = new DateTime(2023, 1, 1);
        decimal saldoAnterior = 2500;

        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);

        // Act
        long retorno = await extratoService.SetSaldoDiaAsync(dados, saldoAnterior);

        bool resultado = retorno > 0;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - SetPagarAsync()")]
    public async Task SetPagarAsync_retorna_true()
    {
        // Arrange
        DateTime dataTransacao = new DateTime(2023, 1, 2);
        PagarCaixa dados = new();
        dados.CategoriaId = 2;
        dados.FornecedorId = 2;
        dados.Descricao = "Serviços de Motoboy";
        dados.Valor = 100;
        dados.Data = dataTransacao;

        decimal saldoAnterior = 2500;

        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);

        // Act
        Extrato retorno = await extratoService.SetPagarAsync(dados, saldoAnterior);

        bool resultado = retorno != null;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - SetReceberAsync()")]
    public async Task SetReceberAsync_retorna_true()
    {
        // Arrange
        DateTime dataTransacao = new DateTime(2023, 1, 2);
        ReceberCaixa dados = new();
        dados.CategoriaId = 1;
        dados.ClienteId = 1;
        dados.Descricao = "Aporte de Capital";
        dados.Valor = 100;
        dados.Data = dataTransacao;

        decimal saldoAnterior = 2500;

        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);

        // Act
        Extrato retorno = await extratoService.SetReceberAsync(dados, saldoAnterior);

        bool resultado = retorno != null;

        // Assert
        Assert.IsTrue(resultado);
    }


}
