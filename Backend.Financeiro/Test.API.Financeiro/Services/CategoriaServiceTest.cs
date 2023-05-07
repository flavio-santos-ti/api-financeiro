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
    [DataRow(2)]
    public async Task Se_o_Id_for_igual_a_Dois_retorna_o_resultado_igual_a_True(long id)
    {
        // Arrange
        CreateCategoriaValidator validator = new CreateCategoriaValidator();
        var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

        // Act
        Categoria retorno = await categoria.GetAsync(id);

        bool resultado = retorno == null;

        // Assert
        Assert.IsTrue(resultado);
    }


    //[TestMethod]
    //[TestCategory("Categoria - Service")]
    //public async Task Se_o_Nome_estiver_em_branco_retorna_Successed_igual_a_False()
    //{
    //    // Arrange 
    //    CreateCategoria dados = new();
    //    dados.Nome = "";
    //    dados.Tipo = "E";

    //    CreateCategoriaValidator validator = new CreateCategoriaValidator();
    //    var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository, _mapper);

    //    // Act
    //    ServiceResult retorno = await categoria.CreateAsync(dados);

    //    // Assert
    //    Assert.IsFalse(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Categoria - Service")]
    //public async Task Se_o_Nome_estiver_null_retorna_Successed_igual_a_False()
    //{
    //    // Arrange 
    //    CreateCategoria dados = new();
    //    dados.Nome = null;
    //    dados.Tipo = "E";

    //    CreateCategoriaValidator validator = new CreateCategoriaValidator();
    //    var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository);

    //    // Act
    //    ServiceResult retorno = await categoria.CreateAsync(dados);

    //    // Assert
    //    Assert.IsFalse(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Categoria - Service")]
    //public async Task Se_o_Tipo_estiver_em_branco_retorna_Successed_igual_a_False()
    //{
    //    // Arrange 
    //    CreateCategoria dados = new();
    //    dados.Nome = "Vendas";
    //    dados.Tipo = "";

    //    CreateCategoriaValidator validator = new CreateCategoriaValidator();
    //    var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository);

    //    // Act
    //    ServiceResult retorno = await categoria.CreateAsync(dados);

    //    // Assert
    //    Assert.IsFalse(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Categoria - Service")]
    //public async Task Se_o_Tipo_estiver_null_retorna_Successed_igual_a_False()
    //{
    //    // Arrange 
    //    CreateCategoria dados = new();
    //    dados.Nome = "Vendas";
    //    dados.Tipo = null;

    //    CreateCategoriaValidator validator = new CreateCategoriaValidator();
    //    var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository);

    //    // Act
    //    ServiceResult retorno = await categoria.CreateAsync(dados);

    //    // Assert
    //    Assert.IsFalse(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Categoria - Service")]
    //[DataRow("Vendas", "e")]
    //[DataRow("Vendas", "s")]
    //[DataRow("Vendas", "A")]
    //[DataRow("Vendas", "Z")]
    //public async Task Se_o_Tipo_estiver_diferente_de_E_e_S_retorna_Successed_igual_a_False(string nome, string tipo)
    //{
    //    // Arrange 
    //    CreateCategoria dados = new();
    //    dados.Nome = nome;
    //    dados.Tipo = tipo;

    //    CreateCategoriaValidator validator = new CreateCategoriaValidator();
    //    var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository);

    //    // Act
    //    ServiceResult retorno = await categoria.CreateAsync(dados);

    //    // Assert
    //    Assert.IsFalse(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Categoria - Service")]
    //[DataRow("Despesas", "S")]
    //public async Task Se_o_Nome_ja_estiver_cadastrado_retorna_Successed_igual_a_False(string nome, string tipo)
    //{
    //    // Arrange
    //    CreateCategoria dados = new();
    //    dados.Nome = nome;
    //    dados.Tipo = tipo;

    //    CreateCategoriaValidator validator = new CreateCategoriaValidator();
    //    var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository);

    //    // Act
    //    ServiceResult retorno = await categoria.CreateAsync(dados);

    //    // Assert
    //    Assert.IsFalse(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Categoria - Service")]
    //[DataRow("Vendas", "E")]
    //public async Task Se_o_Nome_nao_estiver_cadastrado_retorna_Successed_igual_a_True(string nome, string tipo)
    //{
    //    // Arrange 
    //    CreateCategoria dados = new();
    //    dados.Nome = nome;
    //    dados.Tipo = tipo;

    //    CreateCategoriaValidator validator = new CreateCategoriaValidator();
    //    var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository);

    //    // Act
    //    ServiceResult retorno = await categoria.CreateAsync(dados);

    //    // Assert
    //    Assert.IsTrue(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Categoria - Service")]
    //[DataRow(3)]
    //public async Task Se_o_Id_da_Categoria_nao_existir_retorna_Successed_igual_a_False(long id)
    //{
    //    // Arrange 
    //    CreateCategoriaValidator validator = new CreateCategoriaValidator();
    //    var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository);

    //    // Act
    //    ServiceResult retorno = await categoria.DeleteAsync(id);

    //    // Assert
    //    Assert.IsFalse(retorno.Successed);
    //}

    //[TestMethod]
    //[TestCategory("Categoria - Service")]
    //[DataRow(0, 1)]
    //[DataRow(1, 1)]
    //public async Task Devera_retornar_os_dados_conforme_paginacao(int skip, int take)
    //{
    //    // Arrange 
    //    CreateCategoriaValidator validator = new CreateCategoriaValidator();
    //    var categoria = new CategoriaService(validator, _unitOfWork, _categoriaRepository);

    //    // Act
    //    ServiceResult retorno = await categoria.GetViewAllAsync(skip, take);

    //    // Assert
    //    Assert.IsTrue(retorno.Successed);
    //}

}