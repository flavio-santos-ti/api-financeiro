using Api.Crud.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Cliente;

public class Cliente : EntityBase
{
    public long PessoaId { get; set; }
    public DateTime DataInclusao { get; set; }
}
