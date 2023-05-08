using Api.Crud.Domain.Entities.Base;

namespace API.Financeiro.Domain.Fornecedor;

public class Fornecedor : EntityBase
{
    public long PessoaId { get; set; }
    public DateTime DataInclusao { get; set; }
}
