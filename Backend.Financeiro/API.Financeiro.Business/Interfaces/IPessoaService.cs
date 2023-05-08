using API.Financeiro.Domain.Pessoa;

namespace API.Financeiro.Business.Interfaces;

public interface IPessoaService
{
    Task<Pessoa> CreateAsync(string nome);
}
