using Api.Crud.Infra.Data.Context;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Saldo;
using API.Financeiro.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Test.API.Financeiro.Data.Repository;

public class SaldoRepositoryFake : ISaldoRepository
{
    private List<Saldo> _saldos;

    public Task AddAsync(Saldo dados)
    {
        throw new NotImplementedException();
    }

    public async Task<Saldo> GetAsync(Expression<Func<Saldo, bool>> condicao)
    {
        await Task.Run(() => {

            _saldos = new List<Saldo>() {
                new Saldo()
                {
                    Id = 1,
                    Tipo = "I",
                    DataSaldo = new DateTime(2023, 1, 1),
                    Valor = 300,
                    DataInclusao = DateTime.Now,
                    ExtratoId = 1
                },
                new Saldo()
                {
                    Id = 2,
                    Tipo = "F",
                    DataSaldo = new DateTime(2023, 1, 1),
                    Valor = 300,
                    DataInclusao = DateTime.Now,
                    ExtratoId = 1
                },
                new Saldo()
                {
                    Id = 3,
                    Tipo = "I",
                    DataSaldo = new DateTime(2023, 1, 2),
                    Valor = 300,
                    DataInclusao = DateTime.Now,
                    ExtratoId = 1
                }
            };
        });

        var saldo = _saldos.AsQueryable().FirstOrDefault(condicao);

        return saldo;
    }

    public Task<decimal> GetSaldoAsync(DateTime data)
    {
        throw new NotImplementedException();
    }

    public Task<DateTime> GetDataUltimoMovimentoAsync()
    {
        throw new NotImplementedException();
    }

}
