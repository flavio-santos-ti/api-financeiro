using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Services;
using API.Financeiro.Business.Validators.Cliente;
using API.Financeiro.Business.Validators.Fornecedor;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Fornecedor;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using API.Financeiro.Infra.Data.Repositories;
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


    //[TestMethod]
    //[TestCategory("Fornecedor - Service")]
    //[DataRow(1)]
    //public async Task Se_ao_tentar_excluir_o_Id_existir_retorna_Successed_igual_True(long fornecedorId)
    //{
    //    // Arrange 
    //    var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

    //    // Act
    //    ServiceResult retorno = await fornecedor.DeleteAsync(fornecedorId);

    //    // Assert
    //    Assert.IsTrue(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Fornecedor - Service")]
    //[DataRow(0, 24)]
    //public async Task Ao_informar_o_Skip_igual_a_zero_e_Take_igual_24_retorna_Successed_igual_True(int skip, int take)
    //{
    //    // Arrange 
    //    var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

    //    // Act
    //    ServiceResult retorno = await fornecedor.GetViewAllAsync(skip, take);

    //    // Assert
    //    Assert.IsTrue(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Fornecedor - Service")]
    //[DataRow(1)]
    //public async Task Se_o_Id_do_Fornecedor_existir_retorna_True(long id)
    //{
    //    // Arrange 
    //    var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

    //    // Act
    //    bool retorno = await fornecedor.IsValidAsync(id);

    //    // Assert
    //    Assert.IsTrue(retorno);
    //}

    //[TestMethod]
    //[TestCategory("Fornecedor - Service")]
    //[DataRow(2)]
    //public async Task Se_o_Id_do_Fornecedor_nao_existir_retorna_False(long id)
    //{
    //    // Arrange 
    //    var fornecedor = new FornecedorService(_unitOfWork, _pessoaService, _fornecedorRepository);

    //    // Act
    //    bool retorno = await fornecedor.IsValidAsync(id);

    //    // Assert
    //    Assert.IsFalse(retorno);
    //}



}
