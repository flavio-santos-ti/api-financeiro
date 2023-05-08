using API.Financeiro.Domain.Cliente;
using API.Financeiro.Infra.Data.Interfaces;

namespace Test.API.Financeiro.Repository;

public class ClienteRepositoryFake : IClienteRepository
{
    private IEnumerable<ViewCliente> _clientes;
    private IEnumerable<Cliente> _clientesEntity;

    public ClienteRepositoryFake()
    {
    }

    public async Task AddAsync(Cliente newCliente)
    {
        await Task.Delay(1);
    }

    public async Task<int> DeleteAsync(Cliente dados)
    {
        await Task.Delay(1);

        return 1;
    }

    public async Task<Cliente> GetAsync(long id)
    {
        await Task.Run(() =>
        {
            _clientesEntity = new List<Cliente>() {
                new Cliente()
                {
                    Id = 1,
                    PessoaId = 1,
                    DataInclusao = DateTime.Now
                },
                new Cliente()
                {
                    Id = 2,
                    PessoaId = 2,
                    DataInclusao = DateTime.Now
                }
            };
        });
        
        return _clientesEntity.Where(b => b.Id == id).FirstOrDefault();
    }

    public async Task<Cliente> GetByPessoaIdAsync(long pessoaId)
    {
        await Task.Run(() =>
        {
            _clientesEntity = new List<Cliente>() {
                new Cliente()
                {
                    Id = 1,
                    PessoaId = 1,
                    DataInclusao = DateTime.Now
                },
                new Cliente()
                {
                    Id = 2,
                    PessoaId = 2,
                    DataInclusao = DateTime.Now
                }
            };
        });

        return _clientesEntity.Where(b => b.PessoaId == pessoaId).FirstOrDefault();
    }

    public async Task<IEnumerable<ViewCliente>> GetViewAllAsync(int skip, int take)
    {
        await Task.Run(() =>
        {

            _clientes = new List<ViewCliente>() {
                new ViewCliente()
                {
                    Id = 1,
                    PessoaId = 1,
                    Nome = "Flavio",
                    DataInclusao = DateTime.Now
                },
                new ViewCliente()
                {
                    Id = 2,
                    PessoaId = 2,
                    Nome = "Roberto",
                    DataInclusao = DateTime.Now
                }
            };

        });

        return _clientes.Skip(skip).Take(take).ToList();
    }

}
