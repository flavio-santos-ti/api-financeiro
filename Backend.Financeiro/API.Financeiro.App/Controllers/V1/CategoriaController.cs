using API.Financeiro.Business.Interfaces;
using API.Financeiro.Domain.Categoria;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Financeiro.App.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriaController(ICategoriaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCategoria dados)
    {
        var categoria = await _service.CreateAsync(dados);
        
        if (!categoria.Successed)
        {
            return BadRequest(categoria);
        }

        return Ok(categoria);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var categoria = await _service.DeleteAsync(id);

        if (!categoria.Successed)
        {
            return BadRequest(categoria);
        }

        return Ok(categoria);
    }

    [HttpGet("{skip:int}/{take:int}")]
    public async Task<IActionResult> GetViewAllAsync(int skip, int take)
    {
        var usuario = await _service.GetViewAllAsync(skip, take);

        return (!usuario.Successed) ? BadRequest(usuario) : Ok(usuario);
    }


}
