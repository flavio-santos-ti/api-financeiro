using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Business.Interfaces;
using API.Financeiro.Business.Services.Base;
using API.Financeiro.Domain.Pessoa;
using API.Financeiro.Infra.Data.Interfaces;

namespace API.Financeiro.Business.Services;

public class PessoaService : ServiceBase, IPessoaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPessoaRepository _pessoaRepository;

    public PessoaService(IUnitOfWork unitOfWork, IPessoaRepository pessoaRepository)
    {
        _unitOfWork = unitOfWork;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<Pessoa> CreateAsync(string nome)
    {
        string hashNome = base.GetHashMD5(nome);

        Pessoa pessoa = await _pessoaRepository.GetAsync(hashNome);

        if (pessoa == null)
        {
            Pessoa newPessoa = new();
            newPessoa.Id = 3;
            newPessoa.Nome = nome.ToUpper();
            newPessoa.HashNome = hashNome.ToUpper();
            newPessoa.DataInclusao = DateTime.Now;
            await _pessoaRepository.AddAsync(newPessoa);
            await _unitOfWork.SaveAsync();
            return newPessoa;
        }
        else
        {
            return pessoa;
        }
    }
}
