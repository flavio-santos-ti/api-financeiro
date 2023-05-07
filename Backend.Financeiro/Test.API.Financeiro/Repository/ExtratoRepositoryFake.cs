using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Extrato;
using API.Financeiro.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.API.Financeiro.Repository;

public class ExtratoRepositoryFake : IExtratoRepository
{
    public async Task AddAsync(Extrato dados)
    {
        long id = dados.Id;
        await Task.Delay(1);
    }

    public Task<IEnumerable<ViewExtrato>> GetListarAsync(PeriodoExtrato filtro)
    {
        throw new NotImplementedException();
    }
}
