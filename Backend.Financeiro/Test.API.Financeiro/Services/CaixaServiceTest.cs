using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Infra.Data.Interfaces;
using Test.API.Financeiro.Repository;
using Test.API.Financeiro.UnitOfWork;

namespace Test.API.Financeiro.Services;

[TestClass]
public class CaixaServiceTest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISaldoRepository _saldoRepository;
    private readonly IExtratoRepository _extratoRepository;

    public CaixaServiceTest()
    {
        _unitOfWork = new UnitOfWorkFake();
        _saldoRepository = new SaldoRepositoryFake();
        _extratoRepository = new ExtratoRepositoryFake();
    }

    //[TestMethod]
    //[TestCategory("Caixa - Service")]
    //public async Task Se_o_caixa_estiver_Fechado_na_Data_informada_retorna_True()
    //{
    //    // Arrange
    //    DateTime data = new DateTime(2023, 1, 1);
    //    var caixa = new CaixaService(_saldoRepository, _unitOfWork, _extratoRepository);

    //    // Act
    //    bool retorno = await caixa.IsFechadoAsync(data);

    //    // Assert
    //    Assert.IsTrue(retorno);
    //}

    //[TestMethod]
    //[TestCategory("Caixa - Service")]
    //public async Task Se_o_caixa_nao_estiver_Fechado_na_Data_informada_retorna_False()
    //{
    //    // Arrange
    //    DateTime data = new DateTime(2023, 1, 2);
    //    var caixa = new CaixaService(_saldoRepository, _unitOfWork, _extratoRepository);

    //    // Act
    //    bool retorno = await caixa.IsFechadoAsync(data);

    //    // Assert
    //    Assert.IsFalse(retorno);
    //}

    //[TestMethod]
    //[TestCategory("Caixa - Service")]
    //public async Task Se_o_caixa_estiver_Aberto_na_Data_informada_retorna_True()
    //{
    //    // Arrange
    //    DateTime dataInformada = new DateTime(2023, 1, 1);
    //    var caixa = new CaixaService(_saldoRepository, _unitOfWork, _extratoRepository);
    //    // Act
    //    bool retorno = await caixa.IsAbertoAsync(dataInformada);

    //    // Assert
    //    Assert.IsTrue(retorno);
    //}

    //[TestMethod]
    //[TestCategory("Caixa - Service")]
    //public async Task Se_o_caixa_nao_estiver_Aberto_na_Data_informada_retorna_False()
    //{
    //    // Arrange
    //    DateTime dataInformada = new DateTime(2023, 1, 3);
    //    var caixa = new CaixaService(_saldoRepository, _unitOfWork, _extratoRepository);
    //    // Act
    //    bool retorno = await caixa.IsAbertoAsync(dataInformada);

    //    // Assert
    //    Assert.IsFalse(retorno);
    //}

}
