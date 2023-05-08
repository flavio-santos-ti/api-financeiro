namespace API.Financeiro.Domain.Caixa;

public class ReceberCaixa
{
    public long CategoriaId { get; set; }
    public long ClienteId { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; }
    public DateTime Data { get; set; }
}
