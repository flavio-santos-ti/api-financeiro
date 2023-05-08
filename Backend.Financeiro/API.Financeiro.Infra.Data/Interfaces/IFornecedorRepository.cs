using API.Financeiro.Domain.Fornecedor;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface IFornecedorRepository
{
    Task AddAsync(Fornecedor newFornecedor);
    Task<Fornecedor> GetAsync(long id);
    Task<Fornecedor> GetByPessoaIdAsync(long pessoaId);
    Task<int> DeleteAsync(Fornecedor dados);
    Task<IEnumerable<ViewFornecedor>> GetViewAllAsync(int skip, int take);
}
