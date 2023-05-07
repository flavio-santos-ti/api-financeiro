using API.Financeiro.Business.Interfaces;
using API.Financeiro.Domain.Caixa;
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
        //var extrato = await _service.GetListarAsync(filtro);

        //return (!extrato.Successed) ? BadRequest(extrato) : Ok(extrato);
        await Task.Delay(1);
        return Ok(filtro);

    }

    [HttpPost("Abrir")]
    public async Task<IActionResult> AbrirAsync(AbrirCaixa dados)
    {
        var caixa = await _service.AbrirAsync(dados);

        return (!caixa.Successed) ? BadRequest(caixa) : Ok(caixa);

    }

    [HttpPost("Fechar")]
    public async Task<IActionResult> FecharAsync(FecharCaixa dados)
    {
        var caixa = await _service.SetFecharAsync(dados);

        return (!caixa.Successed) ? BadRequest(caixa) : Ok(caixa);

    }

    [HttpPost("Receber")]
    public async Task<IActionResult> ReceberAsync(ReceberCaixa dados)
    {
        var caixa = await _service.SetReceberAsync(dados);

        return (!caixa.Successed) ? BadRequest(caixa) : Ok(caixa);
    }

    [HttpPost("Pagar")]
    public async Task<IActionResult> PagarAsync(PagarCaixa dados)
    {
        var caixa = await _service.SetPagarAsync(dados);

        return (!caixa.Successed) ? BadRequest(caixa) : Ok(caixa);
    }


}
