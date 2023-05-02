using Api.Crud.Infra.Data.Interfaces;
using API.Financeiro.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Services;

public class SaldoService
{
    private readonly ISaldoRepository _saldoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaldoService(ISaldoRepository saldoRepository, IUnitOfWork unitOfWork)
    {
        _saldoRepository = saldoRepository;
        _unitOfWork = unitOfWork;
    }




}
