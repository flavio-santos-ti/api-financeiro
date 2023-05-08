using API.Financeiro.Domain.Pessoa;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface IPessoaRepository
{
    Task AddAsync(Pessoa newPessoa);
    Task<Pessoa> GetAsync(string hashNome);
}
