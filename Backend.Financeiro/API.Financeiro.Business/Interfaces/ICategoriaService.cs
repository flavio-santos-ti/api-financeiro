using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Result;

namespace API.Financeiro.Business.Interfaces;

public interface ICategoriaService
{
    Task<ServiceResult> CreateAsync(CreateCategoria dados);
    Task<ServiceResult> DeleteAsync(long id);
    Task<Categoria> GetAsync(long id);
    Task<ServiceResult> GetViewAllAsync(int skip, int take);
    Task<bool> IsValidAsync(long id);
}
