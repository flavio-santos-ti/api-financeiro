using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Extrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface IExtratoRepository
{
    Task AddAsync(Extrato dados);
    Task<IEnumerable<ViewExtrato>> GetListarAsync(PeriodoExtrato filtro);
}
