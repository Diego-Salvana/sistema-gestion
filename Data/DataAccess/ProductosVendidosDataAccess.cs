using Data.Context;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess;
public class ProductosVendidosDataAccess
{
    private readonly SistemaGestionContext _sistemaGestionContext;

    public ProductosVendidosDataAccess (SistemaGestionContext context)
    {
        _sistemaGestionContext = context;
    }

    public async Task<List<ProductoVendido>> ListarProductosVendidosAsync ()
    {
        return await _sistemaGestionContext.ProductosVendidos.ToListAsync();
    }

    public async Task<ProductoVendido> ObtenerProductoVendidoAsync (int id)
    {
        ProductoVendido? productoVendido = await _sistemaGestionContext.ProductosVendidos
                                                        .Include(pv => pv.Venta)
                                                        .Include(pv => pv.Producto)
                                                        .FirstOrDefaultAsync(pv => pv.Id == id);

        if (productoVendido is null)
        {
            throw new ArgumentException("No existe el producto vendido");
        }

        return productoVendido;
    }

    public async Task<List<ProductoVendido>> ObtenerProductosVendidosByProductosIdsAsync (int productoId)
    {
        return await _sistemaGestionContext.ProductosVendidos
                            .Include(pv => pv.Venta)
                            .Where(pv => pv.Producto.Id == productoId)
                            .ToListAsync();
    }

    public async Task<List<ProductoVendido>> ObtenerProductosVendidosAsync (List<int> ids)
    {
        List<ProductoVendido> productos = await _sistemaGestionContext.ProductosVendidos
                                                        .Where(pv => ids.Contains(pv.Producto.Id))
                                                        .ToListAsync();

        return productos;
    }

    public async Task CrearProductoVendidoAsync (ProductoVendido productoVendido)
    {
        productoVendido.Producto.Stock -= productoVendido.Stock;

        await _sistemaGestionContext.ProductosVendidos.AddAsync(productoVendido);
        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task ModificarProductoVendidoAsync (int id, ProductoVendido productoAcutalizado)
    {
        ProductoVendido productoVendido = await ObtenerProductoVendidoAsync(id);

        productoVendido.Producto = productoAcutalizado.Producto;
        productoVendido.Stock = productoAcutalizado.Stock;
        productoVendido.Venta = productoAcutalizado.Venta;

        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task EliminarProductoVendidoAsync (int id)
    {
        ProductoVendido productoVendido = await ObtenerProductoVendidoAsync(id);

        _sistemaGestionContext.ProductosVendidos.Remove(productoVendido);
        await _sistemaGestionContext.SaveChangesAsync();
    }
}
