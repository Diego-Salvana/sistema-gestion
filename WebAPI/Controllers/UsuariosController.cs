﻿using Bussiness.Services;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly ILogger<UsuariosController> _logger;
    private readonly UsuarioBussiness _usuarioBussiness;

    public UsuariosController (ILogger<UsuariosController> logger, UsuarioBussiness usuarioBussiness)
    {
        _logger = logger;
        _usuarioBussiness = usuarioBussiness;
    }

    [HttpGet]
    public async Task<ActionResult<List<Usuario>>> ListarUsuarios ()
    {
        try
        {
            return Ok(await _usuarioBussiness.ListarUsuariosAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> ObtenerUsuario (int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            return Ok(await _usuarioBussiness.ObtenerUsuarioAsync(id));
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("registrar")]
    public async Task<ActionResult<Usuario>> CrearUsuario (Usuario usuario)
    {
        try
        {
            Usuario nuevoUsuario = await _usuarioBussiness.CrearUsuarioAsync(usuario);

            return CreatedAtAction(
                nameof(ObtenerUsuario),
                new { id = nuevoUsuario.Id },
                nuevoUsuario
            );
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("ingresar")]
    public async Task<ActionResult<Usuario>> Ingresar (IngresoDTO usuario)
    {
        try
        {
            Usuario usuarioResp = await _usuarioBussiness.Ingresar(usuario);

            return Ok(usuarioResp);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Usuario>> ModificarUsuario (int id, Usuario usuarioModificado)
    {
        if (id <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            Usuario usuario = await _usuarioBussiness.ModificarUsuarioAsync(id, usuarioModificado);

            return Ok(usuario);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> EliminarUsuario (int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            await _usuarioBussiness.EliminarUsuarioAsync(id);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
