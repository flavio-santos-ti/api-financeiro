using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Fornecedor;
using API.Financeiro.Domain.Pessoa;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace API.Financeiro.Business.Services;

public class FornecedorService : ServiceBase, IFornecedorService
{
    private readonly IValidator<CreateFornecedor> _validatorCreate;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPessoaService _pessoaService;
    private readonly IFornecedorRepository _fornecedorRepository;

    public FornecedorService(IValidator<CreateFornecedor> validatorCreate, IUnitOfWork unitOfWork, IPessoaService pessoaService, IFornecedorRepository fornecedorRepository)
    {
        _validatorCreate = validatorCreate;
        _unitOfWork = unitOfWork;
        _pessoaService = pessoaService;
        _fornecedorRepository = fornecedorRepository;
    }

    public async Task<ServiceResult> CreateAsync(CreateFornecedor dados)
    {
        ValidationResult result = await _validatorCreate.ValidateAsync(dados);

        if (!result.IsValid) return base.ErrorValidationCreate(result, "Fornecedor");

        await _unitOfWork.BeginTransactionAsync();

        var pessoa = await _pessoaService.CreateAsync(dados.Nome);

        var cliente = await _fornecedorRepository.GetByPessoaIdAsync(pessoa.Id);

        if (cliente != null)
        {
            await _unitOfWork.RolbackAsync();
            return base.ErrorJaExiste("Fornecedor");
        }
        else
        {
            Fornecedor newFornecedor = new();
            newFornecedor.PessoaId = pessoa.Id;
            newFornecedor.DataInclusao = DateTime.Now;
            await _fornecedorRepository.AddAsync(newFornecedor);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();
            return base.SuccessedAdd(newFornecedor, "Fornecedor");
        }
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        await _unitOfWork.BeginTransactionAsync();

        var fornecedor = await _fornecedorRepository.GetAsync(id);

        if (fornecedor == null)
        {
            await _unitOfWork.RolbackAsync();
            return base.ErrorNaoEncontrado("Fornecedor");
        }
        else
        {
            int result = await _fornecedorRepository.DeleteAsync(fornecedor);
            await _unitOfWork.SaveAsync();

            if (result == 1)
            {
                await _unitOfWork.CommitAsync();
                return base.SuccessedDelete("Fornecedor");
            }
            else
            {
                await _unitOfWork.RolbackAsync();
                return base.ErrorDelete("Fornecedor");
            }
        }
    }

    public async Task<Fornecedor> GetAsync(long id)
    {
        var fornecedor = await _fornecedorRepository.GetAsync(id);

        return fornecedor;
    }

    public async Task<ServiceResult> GetViewAllAsync(int skip, int take)
    {
        var fornecedores = await _fornecedorRepository.GetViewAllAsync(skip, take);

        return base.SuccessedViewAll(fornecedores, "Fornecedores", fornecedores.Count());
    }

    public async Task<bool> IsValidAsync(long id)
    {
        var fornecedor = await _fornecedorRepository.GetAsync(id);

        return (fornecedor != null);
    }

}
