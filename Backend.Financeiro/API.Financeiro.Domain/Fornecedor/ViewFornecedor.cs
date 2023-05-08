namespace API.Financeiro.Domain.Fornecedor;

public class ViewFornecedor
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public long PessoaId { get; set; }
    public DateTime DataInclusao { get; set; }
}
