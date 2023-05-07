using API.Financeiro.Domain.Fornecedor;
using API.Financeiro.Domain.Result;

namespace API.Financeiro.Business.Interfaces;

public interface IFornecedorService
{
    Task<ServiceResult> CreateAsync(CreateFornecedor dados);
    Task<ServiceResult> DeleteAsync(long id);
    Task<Fornecedor> GetAsync(long id);
    Task<ServiceResult> GetViewAllAsync(int skip, int take);
    Task<bool> IsValidAsync(long id);
}
