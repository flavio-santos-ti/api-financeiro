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

    Task IClienteRepository.AddAsync(Cliente newCliente)
    {
        throw new NotImplementedException();
    }

    Task<int> IClienteRepository.DeleteAsync(Cliente dados)
    {
        throw new NotImplementedException();
    }

    Task<Cliente> IClienteRepository.GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<Cliente> GetByPessoaIdAsync(long pessoaId)
    {
        await Task.Delay(1);
        
        Cliente cliente = new();
        cliente.PessoaId = pessoaId;
        cliente.DataInclusao = DateTime.Now;
        return cliente;
    }

    Task<IEnumerable<ViewCliente>> IClienteRepository.GetViewAllAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }
}
