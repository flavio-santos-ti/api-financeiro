using API.Financeiro.Domain.Pagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface IPagarRepository
{
    Task AddAsync(Pagar newPagamento);
    Task<IEnumerable<ViewPagar>> GetViewAllAsync(int skip, int take);
}
