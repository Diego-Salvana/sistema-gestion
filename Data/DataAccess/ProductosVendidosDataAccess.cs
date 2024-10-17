using Data.Context;
using Entities;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess;
public class ProductosVendidosDataAccess
{
    private readonly SistemaGestionContext _sistemaGestionContext;

    public ProductosVendidosDataAccess (SistemaGestionContext context)
    {
        _sistemaGestionContext = context;
    }

    public async Task<List<ProductoVendidoDTO>> ListarProductosVendidosAsync ()
    {
        return await _sistemaGestionContext.ProductosVendidos
                                                .Include(pv => pv.Producto)
                                                .Include(pv => pv.Venta)
                                                .ThenInclude(v => v.Usuario)
                                                .Select(pv => new ProductoVendidoDTO(pv))
                                                .ToListAsync();
    }

    public async Task<List<ProductoVendidoDTO>> ListarProductosVendidosAsync (int usuarioId)
    {
        return await _sistemaGestionContext.ProductosVendidos
                                                .Include(pv => pv.Producto)
                                                .Include(pv => pv.Venta)
                                                .ThenInclude(v => v.Usuario)
                                                .Where(pv => pv.Venta.Usuario.Id == usuarioId)
                                                .Select(pv => new ProductoVendidoDTO(pv))
                                                .ToListAsync();
    }

    public async Task<ProductoVendido> ObtenerProductoVendidoAsync (int id)
    {
        ProductoVendido? productoVendido = await _sistemaGestionContext.ProductosVendidos
                                                        .Include(pv => pv.Producto)
                                                        .Include(pv => pv.Venta)
                                                        .ThenInclude(v => v.Usuario)
                                                        .FirstOrDefaultAsync(pv => pv.Id == id);

        if (productoVendido is null)
        {
            throw new ArgumentException("No existe el producto vendido");
        }

        return productoVendido;
    }

    public async Task<List<ProductoVendido>> ObtenerProductosVendidosByProductoIdAsync (int productoId)
    {
        return await _sistemaGestionContext.ProductosVendidos
                            .Include(pv => pv.Venta)
                            .Where(pv => pv.Producto.Id == productoId)
                            .ToListAsync();
    }

    public async Task CrearProductoVendidoAsync (ProductoVendido productoVendido)
    {
        productoVendido.Producto.Stock -= productoVendido.Stock;

        await _sistemaGestionContext.ProductosVendidos.AddAsync(productoVendido);
        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task EliminarProductoVendidoAsync (int id)
    {
        ProductoVendido productoVendido = await ObtenerProductoVendidoAsync(id);

        _sistemaGestionContext.ProductosVendidos.Remove(productoVendido);
        await _sistemaGestionContext.SaveChangesAsync();
    }
}
