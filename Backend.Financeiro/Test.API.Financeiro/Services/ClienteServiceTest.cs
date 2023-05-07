using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using Test.API.Financeiro.Business;
using Test.API.Financeiro.Data.Repository;
using Test.API.Financeiro.Data.UnitOfWork;

namespace Test.API.Financeiro.Services;

[TestClass]
public class ClienteServiceTest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClienteRepository _clienteRepository;
    private readonly IPessoaService _pessoaService;

    public ClienteServiceTest()
    {
        //_unitOfWork = new UnitOfWorkFake();
        //_clienteRepository = new ClienteRepositoryFake();
        //_pessoaService = new PessoaServiceFake();
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
