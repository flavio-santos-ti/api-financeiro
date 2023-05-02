using Api.Crud.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Exrato;

public class Extrato : EntityBase
{
    public string Tipo { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public decimal Saldo { get; set; }
    public decimal ValorRelatorio { get; set; }
    public DateTime DataExtrato { get; set; }
    public DateTime DataInclusao { get; set; }
}
