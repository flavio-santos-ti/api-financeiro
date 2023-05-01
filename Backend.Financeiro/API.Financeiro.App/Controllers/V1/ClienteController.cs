﻿using API.Financeiro.Business.Interfaces;
using API.Financeiro.Domain.Categoria;
using API.Financeiro.Domain.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace API.Financeiro.App.Controllers.V1;


[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCliente dados)
    {
        var cliente = await _service.CreateAsync(dados);

        if (!cliente.Successed)
        {
            return BadRequest(cliente);
        }

        return Ok(cliente);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var cliente = await _service.DeleteAsync(id);

        if (!cliente.Successed)
        {
            return BadRequest(cliente);
        }

        return Ok(cliente);
    }

    [HttpGet("{skip:int}/{take:int}")]
    public async Task<IActionResult> GetViewAllAsync(int skip, int take)
    {
        var cliente = await _service.GetViewAllAsync(skip, take);

        return (!cliente.Successed) ? BadRequest(cliente) : Ok(cliente);
    }
}
