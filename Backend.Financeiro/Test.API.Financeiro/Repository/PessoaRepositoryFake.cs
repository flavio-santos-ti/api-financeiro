using API.Financeiro.Domain.Pessoa;
using API.Financeiro.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.API.Financeiro.Repository;

public class PessoaRepositoryFake : IPessoaRepository
{
    public PessoaRepositoryFake()
    {
    }

    public Task AddAsync(Pessoa newPessoa)
    {
        throw new NotImplementedException();
    }

    public Task<Pessoa> GetAsync(string hashNome)
    {
        throw new NotImplementedException();
    }
}
