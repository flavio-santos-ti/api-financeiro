using Api.Crud.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Categoria;

public class Categoria : EntityBase
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
}
