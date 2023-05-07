using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Fornecedor;
using API.Financeiro.Infra.Data.Interfaces;

namespace Test.API.Financeiro.Repository;

public class FornecedorRepositoryFake : IFornecedorRepository
{
    private IEnumerable<ViewFornecedor> _fornecedores;
    private IEnumerable<Fornecedor> _fornecedoresEntity;

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
        await Task.Run(() =>
        {
            _fornecedoresEntity = new List<Fornecedor>() {
                new Fornecedor()
                {
                    Id = 1,
                    PessoaId = 1,
                    DataInclusao = DateTime.Now
                },
                new Fornecedor()
                {
                    Id = 2,
                    PessoaId = 2,
                    DataInclusao = DateTime.Now
                }
            };
        });

        return _fornecedoresEntity.Where(b => b.Id == id).FirstOrDefault();
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

    public async Task<IEnumerable<ViewFornecedor>> GetViewAllAsync(int skip, int take)
    {
        await Task.Run(() =>
        {

            _fornecedores = new List<ViewFornecedor>() {
                new ViewFornecedor()
                {
                    Id = 1,
                    PessoaId = 1,
                    Nome = "Flavio",
                    DataInclusao = DateTime.Now
                },
                new ViewFornecedor()
                {
                    Id = 2,
                    PessoaId = 2,
                    Nome = "Roberto",
                    DataInclusao = DateTime.Now
                }
            };

        });

        return _fornecedores.Skip(skip).Take(take).ToList();

    }
}
