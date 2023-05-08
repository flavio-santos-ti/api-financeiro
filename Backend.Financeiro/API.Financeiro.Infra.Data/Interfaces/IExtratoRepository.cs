using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Extrato;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface IExtratoRepository
{
    Task AddAsync(Extrato dados);
    Task<IEnumerable<ViewExtrato>> GetListarAsync(PeriodoExtrato filtro);
}
