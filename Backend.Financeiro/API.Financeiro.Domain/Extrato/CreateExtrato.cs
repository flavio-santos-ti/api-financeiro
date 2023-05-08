namespace API.Financeiro.Domain.Extrato;

public class CreateExtrato
{
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataExtrato { get; set; }
}
