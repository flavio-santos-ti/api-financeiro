﻿using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services;
using API.Financeiro.Domain.Fornecedor;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using Test.API.Financeiro.Business;
using Test.API.Financeiro.Data.Repository;
using Test.API.Financeiro.Data.UnitOfWork;

namespace Test.API.Financeiro.Services;

[TestClass]
public class FornecedorServiceTest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IPessoaService _pessoaService;

    public FornecedorServiceTest()
    {
        _unitOfWork = new UnitOfWorkFake();
        _fornecedorRepository = new FornecedorRepositoryFake();
        _pessoaService = new PessoaServiceFake();
    }

    [TestMethod]
    [TestCategory("Fornecedor - Service")]
    public async Task Se_o_Nome_Ja_estiver_cadastrado_retorna_Successed_igual_a_False()
    {
        // Arrange 
        CreateFornecedor dados = new();
        dados.Nome = "Flavio";

        var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

        // Act
        ServiceResult retorno = await fornecedor.CreateAsync(dados);

        // Assert
        Assert.IsFalse(retorno.Successed);
    }

    [TestMethod]
    [TestCategory("Fornecedor - Service")]
    public async Task Se_o_Nome_nao_estiver_cadastrado_retorna_Successed_igual_True()
    {
        // Arrange 
        CreateFornecedor dados = new();
        dados.Nome = "Roberto";


        var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

        // Act
        ServiceResult retorno = await fornecedor.CreateAsync(dados);

        // Assert
        Assert.IsTrue(retorno.Successed);

    }

    [TestMethod]
    [TestCategory("Fornecedor - Service")]
    [DataRow(2)]
    public async Task Se_ao_tentar_Excluir_o_Id_nao_estiver_cadastrado_retorna_Successed_igual_False(long fornecedorId)
    {
        // Arrange 
        var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

        // Act
        ServiceResult retorno = await fornecedor.DeleteAsync(fornecedorId);

        // Assert
        Assert.IsFalse(retorno.Successed);
    }

    [TestMethod]
    [TestCategory("Fornecedor - Service")]
    [DataRow(1)]
    public async Task Se_ao_tentar_excluir_o_Id_existir_retorna_Successed_igual_True(long fornecedorId)
    {
        // Arrange 
        var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

        // Act
        ServiceResult retorno = await fornecedor.DeleteAsync(fornecedorId);

        // Assert
        Assert.IsTrue(retorno.Successed);
    }

    [TestMethod]
    [TestCategory("Fornecedor - Service")]
    [DataRow(0, 24)]
    public async Task Ao_informar_o_Skip_igual_a_zero_e_Take_igual_24_retorna_Successed_igual_True(int skip, int take)
    {
        // Arrange 
        var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

        // Act
        ServiceResult retorno = await fornecedor.GetViewAllAsync(skip, take);

        // Assert
        Assert.IsTrue(retorno.Successed);
    }

    [TestMethod]
    [TestCategory("Fornecedor - Service")]
    [DataRow(1)]
    public async Task Se_o_Id_do_Fornecedor_existir_retorna_True(long id)
    {
        // Arrange 
        var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

        // Act
        bool retorno = await fornecedor.IsValidAsync(id);

        // Assert
        Assert.IsTrue(retorno);
    }

    [TestMethod]
    [TestCategory("Fornecedor - Service")]
    [DataRow(2)]
    public async Task Se_o_Id_do_Fornecedor_nao_existir_retorna_False(long id)
    {
        // Arrange 
        var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

        // Act
        bool retorno = await fornecedor.IsValidAsync(id);

        // Assert
        Assert.IsFalse(retorno);
    }



}