using API.Financeiro.Domain.Caixa;
using API.Financeiro.Domain.Result;

namespace API.Financeiro.Business.Interfaces;

public interface ICaixaService
{
    Task<ServiceResult> AbrirAsync(AbrirCaixa dados);
    Task<ServiceResult> SetFecharAsync(FecharCaixa dados);
    Task<ServiceResult> SetReceberAsync(ReceberCaixa dados);
    Task<ServiceResult> SetPagarAsync(PagarCaixa dados);
}
