using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Caixa;

public class ReceberCaixa
{
    public long ClienteId { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public DateTime Data { get; set; }
}
