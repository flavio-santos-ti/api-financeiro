using API.Financeiro.Domain.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface IClienteRepository
{
    Task AddAsync(Cliente newCliente);
    Task<Cliente> GetAsync(long pessoaId);
    Task<int> DeleteAsync(Cliente dados);
    Task<IEnumerable<ViewCliente>> GetViewAllAsync(int skip, int take);
}
