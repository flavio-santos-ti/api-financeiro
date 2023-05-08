using Api.Crud.Domain.Entities.Base;

namespace API.Financeiro.Domain.Categoria;

public class Categoria : EntityBase
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
}
