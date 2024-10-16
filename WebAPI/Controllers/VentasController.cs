using Bussiness.Services;
using Entities;
using Entities.DTOs;
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
    public async Task<ActionResult<List<VentaDTORespuesta>>> ListarVentas ()
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

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<List<VentaDTORespuesta>>> ListarVentas (int usuarioId)
    {
        try
        {
            return Ok(await _ventaBussiness.ListarVentasAsync(usuarioId));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("usuario/{usuarioId}/venta/{ventaId}")]
    public async Task<ActionResult<Venta>> ObtenerVenta (int ventaId, int usuarioId)
    {
        if (ventaId <= 0 || usuarioId <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            return Ok(await _ventaBussiness.ObtenerVentaAsync(ventaId, usuarioId));
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
    public async Task<ActionResult<Venta>> CrearVenta (VentaDTO ventaDTO)
    {
        if (ventaDTO.ProductosDetalle.IsNullOrEmpty())
        {
            return BadRequest("La lista de productos no debe estar vacía");
        }

        try
        {
            await _ventaBussiness.CrearVentaAsync(ventaDTO);

            return CreatedAtAction(nameof(ObtenerVenta), new { id = ventaDTO.UsuarioId }, ventaDTO);
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

    [HttpPut("usuario/{usuarioId}/venta/{ventaId}")]
    public async Task<ActionResult> ModificarVenta (
        int ventaId,
        int usuarioId,
        VentaDTO.ComentarioTxt ventaDTO)
    {
        if (ventaId <= 0 || usuarioId <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            await _ventaBussiness.ModificarVentaAsync(ventaId, usuarioId, ventaDTO);

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

    [HttpDelete("usuario/{usuarioId}/venta/{ventaId}")]
    public async Task<ActionResult> EliminarVenta (int ventaId, int usuarioId)
    {
        if (ventaId <= 0 || usuarioId <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            await _ventaBussiness.EliminarVentaAsync(ventaId, usuarioId);

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
