﻿using Bussiness.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly ILogger<VentasController> _logger;
    private readonly VentaBussiness _ventaBussiness;

    public VentasController (ILogger<VentasController> logger, VentaBussiness ventaBussiness)
    {
        _logger = logger;
        _ventaBussiness = ventaBussiness;
    }

    [HttpGet]
    public async Task<ActionResult<List<Venta>>> ListarVentas ()
    {
        try
        {
            return Ok(await _ventaBussiness.ListarVentasAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Venta>> ObtenerVenta (int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            return Ok(await _ventaBussiness.ObtenerVentaAsync(id));
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

    [HttpPost]
    public async Task<ActionResult<Venta>> CrearVenta (Venta venta)
    {
        try
        {
            if (venta.Productos.IsNullOrEmpty()) throw new Exception("Lista de productos vacía");

            await _ventaBussiness.CrearVentaAsync(venta);

            return CreatedAtAction(nameof(ObtenerVenta), new { id = venta.Id }, venta);
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
    public async Task<ActionResult> ModificarVenta (int id, Venta venta)
    {
        if (id <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            await _ventaBussiness.ModificarVentaAsync(id, venta);

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

    [HttpDelete("{id}")]
    public async Task<ActionResult> EliminarVenta (int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            await _ventaBussiness.EliminarVentaAsync(id);

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
