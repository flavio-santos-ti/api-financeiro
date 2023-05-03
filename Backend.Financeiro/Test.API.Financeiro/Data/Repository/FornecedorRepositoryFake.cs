using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Fornecedor;
using API.Financeiro.Infra.Data.Interfaces;

namespace Test.API.Financeiro.Data.Repository;

public class FornecedorRepositoryFake : IFornecedorRepository
{
    public async Task AddAsync(Fornecedor newFornecedor)
    {
        await Task.Delay(1);
    }

    public async Task<int> DeleteAsync(Fornecedor dados)
    {
        await Task.Delay(1);

        return 1;
    }

    public async Task<Fornecedor> GetAsync(long id)
    {
        await Task.Delay(1);
        if (id == 1)
        {
            Fornecedor fornecedor = new();
            fornecedor.Id = id;
            fornecedor.PessoaId = 1;
            fornecedor.DataInclusao = DateTime.Now;
            return fornecedor;
        }
        else
        {
            return null;
        }
    }

    public async Task<Fornecedor> GetByPessoaIdAsync(long pessoaId)
    {
        await Task.Delay(1);

        if (pessoaId == 1)
        {
            Fornecedor fornecedor = new();
            fornecedor.PessoaId = pessoaId;
            fornecedor.DataInclusao = DateTime.Now;
            return fornecedor;
        }
        else
        {
            return null;
        }
    }

    public Task<IEnumerable<ViewFornecedor>> GetViewAllAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }
}
