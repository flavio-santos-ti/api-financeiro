namespace API.Financeiro.Domain.Extrato;

public class ViewExtrato
{
    public long Id { get; set; }
    public string Tipo { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataExtrato { get; set; }
    public long TituloId { get; set; }
    public string Nome { get; set; }
}
