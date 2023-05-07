namespace API.Financeiro.Domain.Caixa;

public class PagarCaixa
{
    public long CategoriaId { get; set; }
    public long FornecedorId { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public DateTime Data { get; set; }
}
