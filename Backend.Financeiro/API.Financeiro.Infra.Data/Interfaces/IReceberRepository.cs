using API.Financeiro.Domain.Receber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Infra.Data.Interfaces;

public interface IReceberRepository
{
    Task AddAsync(Receber newRecebimento);
    Task<IEnumerable<ViewReceber>> GetViewAllAsync(int skip, int take);
}
