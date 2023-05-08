using Api.Crud.Domain.Entities.Base;

namespace API.Financeiro.Domain.Pessoa;

public class Pessoa : EntityBase
{
    public string Nome { get; set; }
    public string HashNome { get; set; }
    public DateTime DataInclusao { get; set; }
}
