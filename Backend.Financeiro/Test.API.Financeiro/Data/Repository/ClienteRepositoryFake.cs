using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Infra.Data.Interfaces;
using API.Financeiro.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.API.Financeiro.Data.Repository;

public class ClienteRepositoryFake : IClienteRepository 
{
    private IEnumerable<ViewCliente> _clientes;

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
        await Task.Delay(1);
        if (id == 1)
        {
            Cliente cliente = new();
            cliente.Id = id;
            cliente.PessoaId = 1;
            cliente.DataInclusao = DateTime.Now;
            return cliente;
        }
        else
        {
            return null;
        }
    }

    public async Task<Cliente> GetByPessoaIdAsync(long pessoaId)
    {
        await Task.Delay(1);
        
        if (pessoaId == 1)
        {
            Cliente cliente = new();
            cliente.PessoaId = pessoaId;
            cliente.DataInclusao = DateTime.Now;
            return cliente;
        } else
        {
            return null;
        }
    }

    public async Task<IEnumerable<ViewCliente>> GetViewAllAsync(int skip, int take)
    {
        await Task.Delay(1);

        await Task.Run(() => {

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
