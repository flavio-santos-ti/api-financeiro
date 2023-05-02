using API.Financeiro.Business.Interfaces;
using API.Financeiro.Domain.Pessoa;
using API.Financeiro.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Test.API.Financeiro.Business;

public class PessoaServiceFake : IPessoaService
{
    public PessoaServiceFake()
    {
    }

    public async Task<ServiceResult> CreateAsync(CreatePessoa dados)
    {
        await Task.Delay(1);
        
        var result = new ServiceResult();
        result.Successed = true;
        result.Name = "Pessoa";
        result.Message = "Ok";
        result.Data = null;
        result.ResultId = 1;
        return result;

    }
}
