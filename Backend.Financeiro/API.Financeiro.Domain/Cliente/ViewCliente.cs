using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Cliente;

public class ViewCliente
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public long PessoaId { get; set; }
    public DateTime DataInclusao { get; set; }
}
