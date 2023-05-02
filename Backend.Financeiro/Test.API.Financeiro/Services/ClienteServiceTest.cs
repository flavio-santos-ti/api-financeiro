using Api.Crud.Infra.Data.Interfaces;
using Api.Crud.Infra.Data.UnitOfWork;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services;
using API.Financeiro.Business.Validators.Categoria;
using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        _unitOfWork = new UnitOfWorkFake();
        _clienteRepository = new ClienteRepositoryFake();
        _pessoaService = new PessoaServiceFake();
    }

    [TestMethod]
    [TestCategory("Cliente - Service")]
    public async Task Se_o_Nome_Ja_estiver_cadastrado_retorna_Successed_igual_a_False()
    {
        // Arrange 
        CreateCliente dados = new();
        dados.Nome = "Flavio";
        
        var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

        // Act
        ServiceResult retorno = await cliente.CreateAsync(dados);

        // Assert
        Assert.IsFalse(retorno.Successed);
    }

    [TestMethod]
    [TestCategory("Cliente - Service")]
    public async Task Se_o_Nome_nao_estiver_cadastrado_retorna_Successed_igual_True()
    {
        // Arrange 
        CreateCliente dados = new();
        dados.Nome = "Roberto";
        

        var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

        // Act
        ServiceResult retorno = await cliente.CreateAsync(dados);

        // Assert
        Assert.IsTrue(retorno.Successed);

    }

    [TestMethod]
    [TestCategory("Cliente - Service")]
    [DataRow(2)]
    public async Task Se_o_Id_nao_estiver_cadastrado_retorna_Successed_igual_False(long clientId)
    {
        // Arrange 
        var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

        // Act
        ServiceResult retorno = await cliente.DeleteAsync(clientId);

        // Assert
        Assert.IsFalse(retorno.Successed);
    }

    [TestMethod]
    [TestCategory("Cliente - Service")]
    [DataRow(1)]
    public async Task Se_o_Id_existir_retorna_Successed_igual_true(long clientId)
    {
        // Arrange 
        var cliente = new ClienteService(_unitOfWork, _pessoaService, _clienteRepository);

        // Act
        ServiceResult retorno = await cliente.DeleteAsync(clientId);

        // Assert
        Assert.IsTrue(retorno.Successed);
    }


}
