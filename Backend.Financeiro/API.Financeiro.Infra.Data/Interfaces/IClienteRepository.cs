using API.Financeiro.Domain.Cliente;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface IClienteRepository
{
    Task AddAsync(Cliente newCliente);
    Task<Cliente> GetAsync(long id);
    Task<Cliente> GetByPessoaIdAsync(long pessoaId);
    Task<int> DeleteAsync(Cliente dados);
    Task<IEnumerable<ViewCliente>> GetViewAllAsync(int skip, int take);
}
