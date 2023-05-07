using Api.Crud.Infra.Data.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.API.Financeiro.UnitOfWork;

public class UnitOfWorkFake : IUnitOfWork
{
    public UnitOfWorkFake()
    {
    }

    public async Task BeginTransactionAsync()
    {
        await Task.Delay(1);
    }
    public async Task CommitAsync()
    {
        await Task.Delay(1);
    }

    public async Task RolbackAsync()
    {
        await Task.Delay(1);
    }

    public async Task<int> SaveAsync()
    {
        await Task.Delay(1);
        return 1;
    }

    public string GetTokenSecret()
    {
        return "xtz";
    }


}
