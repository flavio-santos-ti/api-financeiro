using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Pagar;

public class ViewPagar
{
    public long Id { get; set; }
    public long FornecedorId { get; set; }
    public string Favorecido { get; set; }
    public string Descricao { get; set; }
    public decimal ValorReal { get; set; }
    public DateTime DataReal { get; set; }
    public DateTime DataInclusao { get; set; }
}
