using Api.Crud.Infra.Data.Interfaces;
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

}
