using Api.Crud.Infra.Data.Context;
using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Saldo;
using API.Financeiro.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test.API.Financeiro.Repository;

public class SaldoRepositoryFake : ISaldoRepository
{
    private List<Saldo> _saldos;
    private DateTime _dataUltimoMovimento;

    public SaldoRepositoryFake()
    {
        _dataUltimoMovimento = new DateTime(0001, 1, 1);
    }

    public async Task AddAsync(Saldo dados)
    {
        await Task.Delay(1);
        dados.Id += 1;
    }

    public async Task<Saldo> GetAsync(Expression<Func<Saldo, bool>> condicao)
    {
        await Task.Run(() =>
        {

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

    public async Task<DateTime> GetDataUltimoMovimentoAsync()
    {
        await Task.Delay(1);

        return _dataUltimoMovimento;
    }

}
