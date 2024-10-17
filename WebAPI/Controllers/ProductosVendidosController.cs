using Bussiness.Services;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosVendidosController : ControllerBase
{
    private readonly ILogger<ProductosVendidosController> _logger;
    private readonly ProductoVendidoBussiness _productoVendidoBussiness;

    public ProductosVendidosController (ILogger<ProductosVendidosController> logger, ProductoVendidoBussiness productoVendidoBussiness)
    {
        _logger = logger;
        _productoVendidoBussiness = productoVendidoBussiness;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductoVendidoDTO>>> ListarProductosVendidos ()
    {
        try
        {
            return Ok(await _productoVendidoBussiness.ListarProductosVendidosAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<List<ProductoVendidoDTO>>> ListarProductosVendidos (int usuarioId)
    {
        try
        {
            return Ok(await _productoVendidoBussiness.ListarProductosVendidosAsync(usuarioId));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductoVendidoDTO>> ObtenerProductoVendido (int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            return Ok(await _productoVendidoBussiness.ObtenerProductoVendidoAsync(id));
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
