using Api.Crud.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Receber;

public class Receber : EntityBase
{
    public long CategoriaId { get; set; }
    public string Descricao { get; set; }
    public decimal ValorReal { get; set; }
    public DateTime DataReal { get; set; }
    public long ClienteId { get; set; }
    public DateTime DataInclusao { get; set; }
    public long ExtratoId { get; set; }
}
