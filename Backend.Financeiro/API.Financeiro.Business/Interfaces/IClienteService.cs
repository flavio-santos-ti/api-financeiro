using API.Financeiro.Domain.Cliente;
using API.Financeiro.Domain.Result;

namespace API.Financeiro.Business.Interfaces;

public interface IClienteService
{
    Task<ServiceResult> CreateAsync(CreateCliente dados);
    Task<ServiceResult> DeleteAsync(long id);
    Task<Cliente> GetAsync(long id);
    Task<ServiceResult> GetViewAllAsync(int skip, int take);
    Task<bool> IsValidAsync(long id);
}
