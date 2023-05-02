using API.Financeiro.Business.Interfaces;
using API.Financeiro.Domain.Pagar;
using API.Financeiro.Domain.Receber;
using Microsoft.AspNetCore.Mvc;

namespace API.Financeiro.App.Controllers.V1;


[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PagarController : ControllerBase
{
    private readonly IPagarService _service;

    public PagarController(IPagarService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreatePagar dados)
    {
        var recebimento = await _service.CreateAsync(dados);

        if (!recebimento.Successed)
        {
            return BadRequest(recebimento);
        }

        return Ok(recebimento);
    }


}
