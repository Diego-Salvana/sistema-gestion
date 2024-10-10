using Bussiness.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
    public async Task<ActionResult<List<ProductoVendido>>> ListarProductosVendidos ()
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

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductoVendido>> ObtenerProductoVendido (int id)
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

    [HttpPost]
    public async Task<ActionResult<ProductoVendido>> CrearProductoVendido (ProductoVendido productoVendido)
    {
        try
        {
            await _productoVendidoBussiness.CrearProductoVendidoAsync(productoVendido);

            return CreatedAtAction(
                nameof(ObtenerProductoVendido),
                new { id = productoVendido.Id },
                productoVendido
            );
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> ModificarProductoVendido (int id, ProductoVendido productoVendido)
    {
        if (id <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            await _productoVendidoBussiness.ModificarProductoVendidoAsync(id, productoVendido);

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
    public async Task<ActionResult> EliminarProductoVendido (int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id inválido");
        }

        try
        {
            await _productoVendidoBussiness.EliminarProductoVendidoAsync(id);

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
