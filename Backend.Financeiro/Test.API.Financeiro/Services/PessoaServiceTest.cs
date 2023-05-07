using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Services;
using API.Financeiro.Business.Validators.Cliente;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Pessoa;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using API.Financeiro.Infra.Data.Repositories;
using Test.API.Financeiro.Repository;
using Test.API.Financeiro.UnitOfWork;

namespace Test.API.Financeiro.Services;

[TestClass]
public class PessoaServiceTest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPessoaRepository _pessoaRepository;


    public PessoaServiceTest()
    {
        _unitOfWork = new UnitOfWorkFake();
        _pessoaRepository = new PessoaRepositoryFake();
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    [DataRow("Dina")]
    public async Task CreateAsync_Se_o_Nome_Ja_Existir_retorna_o_resultado_igual_a_False(string nome)
    {
        // Arrange
        var pessoa = new PessoaService(_unitOfWork, _pessoaRepository);

        // Act
        Pessoa retorno = await pessoa.CreateAsync(nome);

        bool resultado = retorno == null;

        // Assert
        Assert.IsFalse(resultado);
    }


    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    [DataRow("Roberto")]
    public async Task CreateAsync_Se_o_Nome_Nao_Existir_retorna_o_resultado_igual_a_True(string nome)
    {
        // Arrange
        var pessoa = new PessoaService(_unitOfWork, _pessoaRepository);

        // Act
        Pessoa retorno = await pessoa.CreateAsync(nome);

        bool resultado = retorno != null;

        // Assert
        Assert.IsTrue(resultado);
    }
}
