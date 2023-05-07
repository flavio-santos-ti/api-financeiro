using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Mappers;
using API.Financeiro.Business.Services;
using API.Financeiro.Business.Validators.Categoria;
using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using AutoMapper;
using Test.API.Financeiro.Data.Repository;
using Test.API.Financeiro.Data.UnitOfWork;

namespace Test.API.Financeiro.Services;

[TestClass]
public class CategoriaServiceTest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaServiceTest()
    {
        _unitOfWork = new UnitOfWorkFake();
        _categoriaRepository = new CategoriaRepositoryFake();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CategoriaMapper>());
        _mapper = new Mapper(config);
    }

    [TestMethod]
    [TestCategory("Método - GetViewAllAsync()")]
    [DataRow(0, 2)]
    public async Task Se_o_Skip_for_igual_a_Zero_retorna_resultado_igual_a_False(int skip, int take)
    {
        // Arrange
        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.GetViewAllAsync(skip, take);
        bool resultado = retorno.Count == 2;

        // Assert
        Assert.IsTrue(resultado);
    }


    [TestMethod]
    [TestCategory("Método - GetViewAllAsync()")]
    [DataRow(3, 2)]
    public async Task Se_o_Skip_for_igual_a_Tres_retorna_resultado_igual_a_True(int skip, int take)
    {
        // Arrange
        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.GetViewAllAsync(skip, take);
        bool resultado = retorno.Count == 0;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - GetAsync()")]
    [DataRow(1)]
    public async Task Se_o_Id_for_igual_a_Um_retorna_o_resultado_igual_a_True(long id)
    {
        // Arrange
        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        Categoria retorno = await categoria.GetAsync(id);

        bool resultado = retorno != null;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - GetAsync()")]
    [DataRow(5)]
    public async Task Se_o_Id_Nao_Exisitr_retorna_o_resultado_igual_a_False(long id)
    {
        // Arrange
        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        Categoria retorno = await categoria.GetAsync(id);

        bool resultado = retorno != null;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - IsValidAsync()")]
    [DataRow(1)]
    public async Task IsValidAsync_Se_o_Id_for_igual_a_Um_retorna_o_resultado_igual_a_True(long id)
    {
        // Arrange
        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        bool retorno = await categoria.IsValidAsync(id);

        bool resultado = retorno;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - IsValidAsync()")]
    [DataRow(5)]
    public async Task IsValidAsync_Se_o_Id_Nao_Existir_retorna_o_resultado_igual_a_False(long id)
    {
        // Arrange
        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        bool retorno = await categoria.IsValidAsync(id);

        bool resultado = retorno;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_estiver_Em_Branco_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateCategoria dados = new();
        dados.Nome = "";
        dados.Tipo = "E";

        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_estiver_Null_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateCategoria dados = new();
        dados.Nome = null;
        dados.Tipo = "E";

        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Tipo_estiver_Em_Branco_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateCategoria dados = new();
        dados.Nome = "Vendas";
        dados.Tipo = "";

        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Tipo_estiver_Null_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateCategoria dados = new();
        dados.Nome = "Vendas";
        dados.Tipo = null;

        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    [DataRow("s")]
    [DataRow("e")]
    [DataRow("X")]
    [DataRow("Y")]
    [DataRow("A")]
    public async Task CreateAsync_Se_o_Tipo_estiver_Diferente_S_ou_E_retorna_o_resultado_igual_a_False(string tipo)
    {
        // Arrange
        CreateCategoria dados = new();
        dados.Nome = "Entrada";
        dados.Tipo = tipo;

        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_Ja_Existir_retorna_o_resultado_igual_a_False()
    {
        // Arrange
        CreateCategoria dados = new();
        dados.Nome = "Entradas";
        dados.Tipo = "S";

        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - CreateAsync()")]
    public async Task CreateAsync_Se_o_Nome_Nao_Existir_retorna_o_resultado_igual_a_True()
    {
        // Arrange
        CreateCategoria dados = new();
        dados.Nome = "Vendas";
        dados.Tipo = "E";

        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.CreateAsync(dados);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - DeleteAsync()")]
    [DataRow(8)]
    public async Task DeleteAsync_Se_o_Id_Nao_Existir_retorna_o_resultado_igual_a_False(long id)
    {
        // Arrange
        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.DeleteAsync(id);

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
        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        ServiceResult retorno = await categoria.DeleteAsync(id);

        bool resultado = retorno.Successed;

        // Assert
        Assert.IsTrue(resultado);
    }


}