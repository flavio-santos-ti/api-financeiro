using API.Financeiro.Domain.Pagar;
using API.Financeiro.Domain.Receber;
using API.Financeiro.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Interfaces;

public interface IReceberService
{
    Task<ServiceResult> CreateAsync(CreateReceber dados);
}
