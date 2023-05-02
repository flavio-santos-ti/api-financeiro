using API.Financeiro.Business.Interfaces;
using API.Financeiro.Domain.Extrato;
using Microsoft.AspNetCore.Mvc;

namespace API.Financeiro.App.Controllers.V1;


[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CaixaController : ControllerBase
{
    private readonly ICaixaService _service;

    public CaixaController(ICaixaService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> GetListarAsync(PeriodoExtrato filtro)
    {
        var extrato = await _service.GetListarAsync(filtro);

        return (!extrato.Successed) ? BadRequest(extrato) : Ok(extrato);

    }

}
