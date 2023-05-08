using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Extrato;
using API.Financeiro.Infra.Data.Interfaces;

namespace Test.API.Financeiro.Repository;

public class ExtratoRepositoryFake : IExtratoRepository
{
    public async Task AddAsync(Extrato dados)
    {
        await Task.Delay(1);

        dados.Id += 1;
    }

    public Task<IEnumerable<ViewExtrato>> GetListarAsync(PeriodoExtrato filtro)
    {
        throw new NotImplementedException();
    }
}
