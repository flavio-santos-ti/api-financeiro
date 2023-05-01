using Api.Crud.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Pessoa;

public class Pessoa : EntityBase
{
    public string Nome { get; set; }
    public string HashNome { get; set; }
    public DateTime DataInclusao { get; set; }
}
