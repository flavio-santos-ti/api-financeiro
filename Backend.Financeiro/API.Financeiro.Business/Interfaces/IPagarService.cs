using API.Financeiro.Domain.Pagar;
using API.Financeiro.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Interfaces;

public interface IPagarService
{
    Task<ServiceResult> CreateAsync(CreatePagar dados);
}
