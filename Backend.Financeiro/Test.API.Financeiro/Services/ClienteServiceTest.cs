using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Services;
using API.Financeiro.Business.Validators.Cliente;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using Test.API.Financeiro.Repository;
using Test.API.Financeiro.UnitOfWork;

namespace Test.API.Financeiro.Services;

[TestClass]
public class ClienteServiceTest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClienteRepository _clienteRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public ClienteServiceTest()
    {
        _unitOfWork = new UnitOfWorkFake();
        _clienteRepository = new ClienteRepositoryFake();
        _pessoaRepository = new PessoaRepositoryFake();
    }

    [TestMethod]
    [TestCategory("Método - GetViewAllAsync()")]
    [DataRow(0, 2)]
    public async Task GetViewAllAsync_Se_o_Skip_for_igual_a_Zero_retorna_resultado_igual_a_True(int skip, int take)
    {
        // Arrange
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        // Act
        ServiceResult retorno = await cliente.GetViewAllAsync(skip, take);
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
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        // Act
        ServiceResult retorno = await cliente.GetViewAllAsync(skip, take);
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
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        // Act
        Cliente retorno = await cliente.GetAsync(id);

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
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        // Act
        Cliente retorno = await cliente.GetAsync(id);

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
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        // Act
        bool retorno = await cliente.IsValidAsync(id);

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
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        // Act
        bool retorno = await cliente.IsValidAsync(id);

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
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        // Act
        ServiceResult retorno = await cliente.DeleteAsync(id);

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
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        // Act
        ServiceResult retorno = await cliente.DeleteAsync(id);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_Ja_Existir_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);
        
        CreateCliente dados = new();
        dados.Nome = "Dina";

        // Act
        ServiceResult retorno = await cliente.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_Nao_Existir_retorna_o_resultado_igual_a_True()
    {
        // Arrange
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        CreateCliente dados = new();
        dados.Nome = "Roberto";

        // Act
        ServiceResult retorno = await cliente.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_Estiver_Em_Branco_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        CreateCliente dados = new();
        dados.Nome = "";

        // Act
        ServiceResult retorno = await cliente.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_Estiver_Null_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateClienteValidator validator = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var cliente = new ClienteService(validator, _unitOfWork, pessoaService, _clienteRepository);

        CreateCliente dados = new();
        dados.Nome = null;

        // Act
        ServiceResult retorno = await cliente.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }
}
