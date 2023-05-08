namespace API.Financeiro.Domain.Cliente;

public class ViewCliente
{
    public long Id { get; set; }
    public string Nome { get; set; }
    public long PessoaId { get; set; }
    public DateTime DataInclusao { get; set; }
}
