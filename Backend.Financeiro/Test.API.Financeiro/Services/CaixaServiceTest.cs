using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Mappers;
using API.Financeiro.Business.Services;
using API.Financeiro.Business.Validators.Categoria;
using API.Financeiro.Business.Validators.Cliente;
using API.Financeiro.Business.Validators.Fornecedor;
using API.Financeiro.Domain.Caixa;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using AutoMapper;
using Test.API.Financeiro.Repository;
using Test.API.Financeiro.UnitOfWork;

namespace Test.API.Financeiro.Services;

[TestClass]
public class CaixaServiceTest
{
    private readonly IUnitOfWork _unitOfWork;
    private ISaldoRepository _saldoRepository;
    private readonly IExtratoRepository _extratoRepository;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IFornecedorRepository _fornecedorRepository;

    public CaixaServiceTest()
    {
        _unitOfWork = new UnitOfWorkFake();
        _extratoRepository = new ExtratoRepositoryFake();
        _categoriaRepository = new CategoriaRepositoryFake();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<ClienteMapper>());
        _mapper = new Mapper(config);
        _pessoaRepository = new PessoaRepositoryFake();
        _clienteRepository = new ClienteRepositoryFake();
        _fornecedorRepository = new FornecedorRepositoryFake();
        DateTime DataInformada = new DateTime(0001, 1, 1);
        _saldoRepository = new SaldoRepositoryFake(DataInformada);

    }

    [TestMethod]
    [TestCategory("Método - AbrirAsync()")]
    [DataRow(2, 1, 2023)]
    public async Task AbrirAsync_Se_o_caixa_ja_estiver_aberto_retorna_False(int dia, int mes, int ano)
    {
        // Arrange
        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);
        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);

        CreateCategoriaValidator validatorCategoria = new CreateCategoriaValidator();
        var categoriaService = new CategoriaService(validatorCategoria, _unitOfWork, _categoriaRepository, _mapper);

        CreateClienteValidator validatorCliente = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var clienteService = new ClienteService(validatorCliente, _unitOfWork, pessoaService, _clienteRepository);

        CreateFornecedorValidator validatorFornecedor = new CreateFornecedorValidator();
        var fornecedorService = new FornecedorService(validatorFornecedor, _unitOfWork, pessoaService, _fornecedorRepository);

        var caixaService = new CaixaService(_unitOfWork, extratoService, saldoService, categoriaService, clienteService, fornecedorService);

        AbrirCaixa dados = new();
        dados.DataInformada = new DateTime(ano, mes, dia);

        // Act
        ServiceResult retorno = await caixaService.AbrirAsync(dados);
        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }


    [TestMethod]
    [TestCategory("Método - AbrirAsync()")]
    [DataRow(1, 1, 2023)]
    public async Task AbrirAsync_Se_o_caixa_ja_estiver_fechado_retorna_False(int dia, int mes, int ano)
    {
        // Arrange
        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);
        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);

        CreateCategoriaValidator validatorCategoria = new CreateCategoriaValidator();
        var categoriaService = new CategoriaService(validatorCategoria, _unitOfWork, _categoriaRepository, _mapper);

        CreateClienteValidator validatorCliente = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var clienteService = new ClienteService(validatorCliente, _unitOfWork, pessoaService, _clienteRepository);

        CreateFornecedorValidator validatorFornecedor = new CreateFornecedorValidator();
        var fornecedorService = new FornecedorService(validatorFornecedor, _unitOfWork, pessoaService, _fornecedorRepository);

        var caixaService = new CaixaService(_unitOfWork, extratoService, saldoService, categoriaService, clienteService, fornecedorService);

        AbrirCaixa dados = new();
        dados.DataInformada = new DateTime(ano, mes, dia);

        // Act
        ServiceResult retorno = await caixaService.AbrirAsync(dados);
        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - AbrirAsync()")]
    [DataRow(3, 1, 2023)]
    public async Task AbrirAsync_Se_o_caixa_nao_estiver_fechado_e_nao_estiver_aberto_retorna_True(int dia, int mes, int ano)
    {
        // Arrange
        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);
        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);

        CreateCategoriaValidator validatorCategoria = new CreateCategoriaValidator();
        var categoriaService = new CategoriaService(validatorCategoria, _unitOfWork, _categoriaRepository, _mapper);

        CreateClienteValidator validatorCliente = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var clienteService = new ClienteService(validatorCliente, _unitOfWork, pessoaService, _clienteRepository);

        CreateFornecedorValidator validatorFornecedor = new CreateFornecedorValidator();
        var fornecedorService = new FornecedorService(validatorFornecedor, _unitOfWork, pessoaService, _fornecedorRepository);

        var caixaService = new CaixaService(_unitOfWork, extratoService, saldoService, categoriaService, clienteService, fornecedorService);

        AbrirCaixa dados = new();
        dados.DataInformada = new DateTime(ano, mes, dia);

        // Act
        ServiceResult retorno = await caixaService.AbrirAsync(dados);
        bool resultado = retorno.Successed;

        // Assert
        Assert.IsTrue(resultado);
    }


    [TestMethod]
    [TestCategory("Método - SetFecharAsync()")]
    [DataRow(1, 1, 2023)]
    public async Task SetFecharAsync_Se_o_caixa_ja_estiver_fechado_retorna_False(int dia, int mes, int ano)
    {
        // Arrange
        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);
        var saldoService = new SaldoService(_saldoRepository, _unitOfWork);

        CreateCategoriaValidator validatorCategoria = new CreateCategoriaValidator();
        var categoriaService = new CategoriaService(validatorCategoria, _unitOfWork, _categoriaRepository, _mapper);

        CreateClienteValidator validatorCliente = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var clienteService = new ClienteService(validatorCliente, _unitOfWork, pessoaService, _clienteRepository);

        CreateFornecedorValidator validatorFornecedor = new CreateFornecedorValidator();
        var fornecedorService = new FornecedorService(validatorFornecedor, _unitOfWork, pessoaService, _fornecedorRepository);

        var caixaService = new CaixaService(_unitOfWork, extratoService, saldoService, categoriaService, clienteService, fornecedorService);

        FecharCaixa dados = new();
        dados.DataInformada = new DateTime(ano, mes, dia);

        // Act
        ServiceResult retorno = await caixaService.SetFecharAsync(dados);
        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - SetFecharAsync()")]
    [DataRow(2, 1, 2023)]
    public async Task SetFecharAsync_Se_o_caixa_estiver_aberto_retorna_True(int dia, int mes, int ano)
    {
        FecharCaixa dados = new();
        dados.DataInformada = new DateTime(ano, mes, dia);

        ISaldoRepository saldoRepository = new SaldoRepositoryFake(dados.DataInformada);

        // Arrange
        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);
        var saldoService = new SaldoService(saldoRepository, _unitOfWork);

        CreateCategoriaValidator validatorCategoria = new CreateCategoriaValidator();
        var categoriaService = new CategoriaService(validatorCategoria, _unitOfWork, _categoriaRepository, _mapper);

        CreateClienteValidator validatorCliente = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var clienteService = new ClienteService(validatorCliente, _unitOfWork, pessoaService, _clienteRepository);

        CreateFornecedorValidator validatorFornecedor = new CreateFornecedorValidator();
        var fornecedorService = new FornecedorService(validatorFornecedor, _unitOfWork, pessoaService, _fornecedorRepository);

        var caixaService = new CaixaService(_unitOfWork, extratoService, saldoService, categoriaService, clienteService, fornecedorService);

        // Act
        ServiceResult retorno = await caixaService.SetFecharAsync(dados);
        bool resultado = retorno.Successed;

        // Assert
        Assert.IsTrue(resultado);
    }

    [TestMethod]
    [TestCategory("Método - SetFecharAsync()")]
    [DataRow(3, 1, 2023)]
    public async Task SetFecharAsync_Se_o_caixa_nao_estiver_aberto_retorna_False(int dia, int mes, int ano)
    {
        FecharCaixa dados = new();
        dados.DataInformada = new DateTime(ano, mes, dia);

        ISaldoRepository saldoRepository = new SaldoRepositoryFake(dados.DataInformada);

        // Arrange
        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);
        var saldoService = new SaldoService(saldoRepository, _unitOfWork);

        CreateCategoriaValidator validatorCategoria = new CreateCategoriaValidator();
        var categoriaService = new CategoriaService(validatorCategoria, _unitOfWork, _categoriaRepository, _mapper);

        CreateClienteValidator validatorCliente = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var clienteService = new ClienteService(validatorCliente, _unitOfWork, pessoaService, _clienteRepository);

        CreateFornecedorValidator validatorFornecedor = new CreateFornecedorValidator();
        var fornecedorService = new FornecedorService(validatorFornecedor, _unitOfWork, pessoaService, _fornecedorRepository);

        var caixaService = new CaixaService(_unitOfWork, extratoService, saldoService, categoriaService, clienteService, fornecedorService);

        // Act
        ServiceResult retorno = await caixaService.SetFecharAsync(dados);
        bool resultado = retorno.Successed;

        // Assert
        Assert.IsFalse(resultado);
    }

    [TestMethod]
    [TestCategory("Método - SetReceberAsync()")]
    [DataRow(1)]
    public async Task SetReceberAsync_retorna_True(long CategoriaId)
    {
        DateTime dataTransacao = new DateTime(2023, 1, 2);
        ReceberCaixa dados = new();
        dados.CategoriaId = CategoriaId;
        dados.ClienteId = 1;
        dados.Descricao = "Aporte de Capital";
        dados.Valor = 1500;
        dados.Data = dataTransacao;

        ISaldoRepository saldoRepository = new SaldoRepositoryFake(dataTransacao);

        // Arrange
        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);
        var saldoService = new SaldoService(saldoRepository, _unitOfWork);

        CreateCategoriaValidator validatorCategoria = new CreateCategoriaValidator();
        var categoriaService = new CategoriaService(validatorCategoria, _unitOfWork, _categoriaRepository, _mapper);

        CreateClienteValidator validatorCliente = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var clienteService = new ClienteService(validatorCliente, _unitOfWork, pessoaService, _clienteRepository);

        CreateFornecedorValidator validatorFornecedor = new CreateFornecedorValidator();
        var fornecedorService = new FornecedorService(validatorFornecedor, _unitOfWork, pessoaService, _fornecedorRepository);

        var caixaService = new CaixaService(_unitOfWork, extratoService, saldoService, categoriaService, clienteService, fornecedorService);

        // Act
        ServiceResult retorno = await caixaService.SetReceberAsync(dados);
        bool resultado = retorno.Successed;

        // Assert
        Assert.IsTrue(resultado);
    }


    [TestMethod]
    [TestCategory("Método - SetPagarAsync()")]
    [DataRow(2)]
    public async Task SetPagarAsync_retorna_True(long CategoriaId)
    {
        // Arrange
        DateTime dataTransacao = new DateTime(2023, 1, 2);
        PagarCaixa dados = new();
        dados.CategoriaId = CategoriaId;
        dados.FornecedorId = 2;
        dados.Descricao = "Serviços de Motoboy";
        dados.Valor = 100;
        dados.Data = dataTransacao;

        ISaldoRepository saldoRepository = new SaldoRepositoryFake(dataTransacao);

        var extratoService = new ExtratoService(_extratoRepository, _unitOfWork);
        var saldoService = new SaldoService(saldoRepository, _unitOfWork);

        CreateCategoriaValidator validatorCategoria = new CreateCategoriaValidator();
        var categoriaService = new CategoriaService(validatorCategoria, _unitOfWork, _categoriaRepository, _mapper);

        CreateClienteValidator validatorCliente = new CreateClienteValidator();
        var pessoaService = new PessoaService(_unitOfWork, _pessoaRepository);
        var clienteService = new ClienteService(validatorCliente, _unitOfWork, pessoaService, _clienteRepository);

        CreateFornecedorValidator validatorFornecedor = new CreateFornecedorValidator();
        var fornecedorService = new FornecedorService(validatorFornecedor, _unitOfWork, pessoaService, _fornecedorRepository);

        var caixaService = new CaixaService(_unitOfWork, extratoService, saldoService, categoriaService, clienteService, fornecedorService);

        // Act
        ServiceResult retorno = await caixaService.SetPagarAsync(dados);
        bool resultado = retorno.Successed;

        // Assert
        Assert.IsTrue(resultado);
    }
}
