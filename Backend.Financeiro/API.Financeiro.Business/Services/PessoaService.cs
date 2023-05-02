using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Pessoa;
using API.Financeiro.Domain.Result;
using API.Financeiro.Infra.Data.Interfaces;
using API.Financeiro.Infra.Data.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace API.Financeiro.Business.Services;

public class PessoaService : ServiceBase, IPessoaService
{
    private readonly IValidator<CreatePessoa> _validatorCreate;
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PessoaService(IValidator<CreatePessoa> validatorCreate, IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork)
    {
        _validatorCreate = validatorCreate;
        _pessoaRepository = pessoaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult> CreateAsync(CreatePessoa dados)
    {
        ValidationResult result = await _validatorCreate.ValidateAsync(dados);

        if (!result.IsValid)
        {
            return base.ErrorValidationCreate(result, "Pessoa");
        }

        string hashNome = base.GetHashMD5(dados.Nome);

        Pessoa pessoa = await _pessoaRepository.GetAsync(hashNome);

        if (pessoa == null)
        {
            Pessoa newPessoa = new();
            newPessoa.Nome = dados.Nome.ToUpper();
            newPessoa.HashNome = hashNome.ToUpper();
            newPessoa.DataInclusao = DateTime.Now;
            await _pessoaRepository.AddAsync(newPessoa);
            await _unitOfWork.SaveAsync();
            return base.SuccessedAddId(newPessoa.Id, "Pessoa");
        }
        else
        {
            return base.SuccessedAdd(pessoa.Id, "Pessoa");
        }
    }
}
