using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Services;
using API.Financeiro.Business.Validators.Fornecedor;
using API.Financeiro.Domain.Fornecedor;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using Test.API.Financeiro.Repository;
using Test.API.Financeiro.UnitOfWork;

namespace Test.API.Financeiro.Services;

[TestClass] 
public class FornecedorServiceTest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public FornecedorServiceTest()
    {
        _unitOfWork = new UnitOfWorkFake();
        _fornecedorRepository = new FornecedorRepositoryFake();
        _pessoaRepository = new PessoaRepositoryFake();
    }

    [TestMethod]
    [TestCategory("Método - GetViewAllAsync()")]
    [DataRow(0, 2)]
    public async Task GetViewAllAsync_Se_o_Skip_for_igual_a_Zero_retorna_resultado_igual_a_True(int skip, int take)
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        // Act
        ServiceResult retorno = await fornecedor.GetViewAllAsync(skip, take);
        bool resultado = retorno.Count == 2;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - GetViewAllAsync()")]
    [DataRow(3, 2)]
    public async Task GetViewAllAsync_Se_o_Skip_for_igual_a_Tres_retorna_resultado_igual_a_False(int skip, int take)
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        // Act
        ServiceResult retorno = await fornecedor.GetViewAllAsync(skip, take);
        bool resultado = retorno.Count > 0;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - GetAsync()")]
    [DataRow(1)]
    public async Task GetAsync_Se_o_Id_Existir_retorna_o_resultado_igual_a_True(long id)
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        // Act
        Fornecedor retorno = await fornecedor.GetAsync(id);

        bool resultado = retorno != null;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - GetAsync()")]
    [DataRow(5)]
    public async Task GetAsync_Se_o_Id_Nao_Existir_retorna_o_resultado_igual_a_False(long id)
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        // Act
        Fornecedor retorno = await fornecedor.GetAsync(id);

        bool resultado = retorno != null;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - IsValidAsync()")]
    [DataRow(1)]
    public async Task IsValidAsync_Se_o_Id_Existir_retorna_o_resultado_igual_a_True(long id)
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        // Act
        bool retorno = await fornecedor.IsValidAsync(id);

        bool resultado = retorno;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - IsValidAsync()")]
    [DataRow(5)]
    public async Task IsValidAsync_Se_o_Nao_Id_Existir_retorna_o_resultado_igual_a_False(long id)
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        // Act
        bool retorno = await fornecedor.IsValidAsync(id);

        bool resultado = retorno;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - DeleteAsync()")]
    [DataRow(5)]
    public async Task DeleteAsync_Se_o_Id_Nao_Existir_retorna_o_resultado_igual_a_False(long id)
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        // Act
        ServiceResult retorno = await fornecedor.DeleteAsync(id);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - DeleteAsync()")]
    [DataRow(2)]
    public async Task DeleteAsync_Se_o_Id_Existir_retorna_o_resultado_igual_a_True(long id)
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        // Act
        ServiceResult retorno = await fornecedor.DeleteAsync(id);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_Ja_Existir_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        CreateFornecedor dados = new();
        dados.Nome = "Dina";

        // Act
        ServiceResult retorno = await fornecedor.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_Nao_Existir_retorna_o_resultado_igual_a_True()
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        CreateFornecedor dados = new();
        dados.Nome = "Roberto";

        // Act
        ServiceResult retorno = await fornecedor.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_Estiver_Em_Branco_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        CreateFornecedor dados = new();
        dados.Nome = "";

        // Act
        ServiceResult retorno = await fornecedor.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_Estiver_Null_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateFornecedorValidator validator = new CreateFornecedorValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var fornecedor = new FornecedorService(validator, _unitOfWork, pessoaService, _fornecedorRepository);

        CreateFornecedor dados = new();
        dados.Nome = null;

        // Act
        ServiceResult retorno = await fornecedor.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }
}
