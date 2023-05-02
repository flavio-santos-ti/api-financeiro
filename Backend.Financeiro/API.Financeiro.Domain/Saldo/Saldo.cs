using Api.Crud.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Saldo;

public class Saldo : EntityBase
{
    public DateTime DataSaldo { get; set; }
    public string Tipo { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataInclusao { get; set; }
    public long ExtratoId { get; set; }
}
