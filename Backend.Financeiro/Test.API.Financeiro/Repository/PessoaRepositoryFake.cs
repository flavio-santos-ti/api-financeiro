using API.Financeiro.Domain.Cliente;
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
    private IEnumerable<Pessoa> _pessoas;

    public PessoaRepositoryFake()
    {
    }

    public async Task AddAsync(Pessoa newPessoa)
    {
        await Task.Delay(1);
    }

    public async Task<Pessoa> GetAsync(string hashNome)
    {
        await Task.Run(() =>
        {
            _pessoas = new List<Pessoa>() {
                new Pessoa()
                {
                    Id = 1,
                    Nome = "Flavio",
                    HashNome = "E6A25AD746BB0923F593E94F5128D13D",
                    DataInclusao = DateTime.Now
                },
                new Pessoa()
                {
                    Id = 2,
                    Nome = "Dina",
                    HashNome = "7F3F50F5FDA824900EF8B87E1B3B0A6F",
                    DataInclusao = DateTime.Now
                }
            };
        });

        return _pessoas.Where(b => b.HashNome == hashNome).FirstOrDefault();
    }
}
