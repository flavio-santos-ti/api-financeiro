using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Interfaces;

public interface ICategoriaService
{
    Task<ServiceResult> CreateAsync(CreateCategoria dados);
    Task<ServiceResult> DeleteAsync(long id);
    Task<ServiceResult> GetViewAllAsync(int skip, int take);
}
