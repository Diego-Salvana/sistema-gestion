using Bussiness.Services;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ILogger<ProductosController> _logger;
    private readonly ProductoBussiness _productoBussiness;

    public ProductosController (
        ILogger<ProductosController> logger,
        ProductoBussiness productoBussiness)
    {
        _logger = logger;
        _productoBussiness = productoBussiness;
    }

    [HttpGet]
    public async Task<ActionResult<List<Producto>>> ListarProductos ()
    {
        try
        {
            return Ok(await _productoBussiness.ListarProductosAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<ActionResult<List<Producto>>> ListarProductos (int usuarioId)
    {
        try
        {
            return Ok(await _productoBussiness.ListarProductosAsync(usuarioId));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> ObtenerProducto (int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            return Ok(await _productoBussiness.ObtenerProductoAsync(id));
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
    public async Task<ActionResult<Producto>> CrearProducto (
        [FromHeader] int usuarioId,
        ProductoDTO productoDTO)
    {
        try
        {
            await _productoBussiness.CrearProductoAsync(usuarioId, productoDTO);

            return CreatedAtAction(
                nameof(ObtenerProducto),
                new { id = usuarioId },
                productoDTO
            );
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{productoId}")]
    public async Task<ActionResult> ModificarProducto (
        int productoId,
        [FromHeader] int usuarioId,
        ProductoDTO productoDTO)
    {
        if (productoId <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            await _productoBussiness.ModificarProductoAsync(productoId, usuarioId, productoDTO);

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

    [HttpDelete("{productoId}")]
    public async Task<ActionResult> EliminarProducto (int productoId, [FromHeader] int usuarioId)
    {
        if (productoId <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            await _productoBussiness.EliminarProductoAsync(productoId, usuarioId);

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
