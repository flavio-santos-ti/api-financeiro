using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Mappers;
using API.Financeiro.Business.Services;
using API.Financeiro.Business.Validators.Categoria;
using API.Financeiro.Business.Validators.Cliente;
using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using API.Financeiro.Infra.Data.Repositories;
using AutoMapper;
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


    //[TestMethod]
    //[TestCategory("Cliente - Service")]
    //public async Task Se_o_Nome_Ja_estiver_cadastrado_retorna_Successed_igual_a_False()
    //{
    //    // Arrange 
    //    CreateCliente dados = new();
    //    dados.Nome = "Flavio";

    //    var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

    //    // Act
    //    ServiceResult retorno = await cliente.CreateAsync(dados);

    //    // Assert
    //    Assert.IsFalse(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Cliente - Service")]
    //public async Task Se_o_Nome_nao_estiver_cadastrado_retorna_Successed_igual_True()
    //{
    //    // Arrange 
    //    CreateCliente dados = new();
    //    dados.Nome = "Roberto";


    //    var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

    //    // Act
    //    ServiceResult retorno = await cliente.CreateAsync(dados);

    //    // Assert
    //    Assert.IsTrue(retorno.Successed);

    //}

    //[TestMethod]
    //[TestCategory("Cliente - Service")]
    //[DataRow(2)]
    //public async Task Se_ao_tentar_Excluir_o_Id_nao_estiver_cadastrado_retorna_Successed_igual_False(long clientId)
    //{
    //    // Arrange 
    //    var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

    //    // Act
    //    ServiceResult retorno = await cliente.DeleteAsync(clientId);

    //    // Assert
    //    Assert.IsFalse(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Cliente - Service")]
    //[DataRow(1)]
    //public async Task Se_o_Id_existir_retorna_Successed_igual_True(long clientId)
    //{
    //    // Arrange 
    //    var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

    //    // Act
    //    ServiceResult retorno = await cliente.DeleteAsync(clientId);

    //    // Assert
    //    Assert.IsTrue(retorno.Successed);
    //}

    //[TestMethod]
    //[DataRow(0, 24)]
    //[TestCategory("Cliente - Service")]
    //public async Task Ao_chamar_o_metodo_GetViewAllAsync_retorna_Successed_igual_True(int skip, int take)
    //{
    //    // Arrange 
    //    var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

    //    // Act
    //    ServiceResult retorno = await cliente.GetViewAllAsync(skip, take);

    //    // Assert
    //    Assert.IsTrue(retorno.Successed);
    //}

    //[TestMethod]
    //[DataRow(1)]
    //[TestCategory("Cliente - Service")]
    //public async Task Se_o_Id_existir_retorna_igual_True(int id)
    //{
    //    // Arrange 
    //    var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

    //    // Act
    //    bool retorno = await cliente.IsValidAsync(id);

    //    // Assert
    //    Assert.IsTrue(retorno);
    //}

    //[TestMethod]
    //[DataRow(2)]
    //[TestCategory("Cliente - Service")]
    //public async Task Se_o_Id_nao_existir_retorna_igual_False(int id)
    //{
    //    // Arrange 
    //    var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

    //    // Act
    //    bool retorno = await cliente.IsValidAsync(id);

    //    // Assert
    //    Assert.IsFalse(retorno);
    //}

}
