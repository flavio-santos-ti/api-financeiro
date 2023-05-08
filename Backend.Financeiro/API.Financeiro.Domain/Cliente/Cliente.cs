using Api.Crud.Domain.Entities.Base;

namespace API.Financeiro.Domain.Cliente;

public class Cliente : EntityBase
{
    public long PessoaId { get; set; }
    public DateTime DataInclusao { get; set; }
}
