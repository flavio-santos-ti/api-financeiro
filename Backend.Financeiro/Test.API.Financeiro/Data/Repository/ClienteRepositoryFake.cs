using API.Financeiro.Domain.Cliente;
using API.Financeiro.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.API.Financeiro.Data.Repository;

public class ClienteRepositoryFake : IClienteRepository 
{
    public ClienteRepositoryFake()
    {
    }

    async Task IClienteRepository.AddAsync(Cliente newCliente)
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

    Task<IEnumerable<ViewCliente>> IClienteRepository.GetViewAllAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }
}
