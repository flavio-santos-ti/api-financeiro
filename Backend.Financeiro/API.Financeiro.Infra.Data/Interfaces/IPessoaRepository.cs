using API.Financeiro.Domain.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface IPessoaRepository
{
    Task AddAsync(Pessoa newPessoa);
    Task<Pessoa> GetAsync(string hashNome);
}
