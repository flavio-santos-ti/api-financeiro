using API.Financeiro.Domain.Pessoa;
using API.Financeiro.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Interfaces;

public interface IPessoaService
{
    Task<Pessoa> CreateAsync(string nome);
}
