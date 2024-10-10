using Data.Context;
using Data.Migrations;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Data.DataAccess;
public class ProductosDataAccess
{
    private readonly SistemaGestionContext _sistemaGestionContext;

    public ProductosDataAccess (SistemaGestionContext context)
    {
        _sistemaGestionContext = context;
    }

    public async Task<List<Producto>> ListarProductosAsync ()
    {
        return await _sistemaGestionContext.Productos
                            .Include(p => p.Usuario)
                            .ToListAsync();
    }

    public async Task<Producto> ObtenerProductoAsync (int id)
    {
        Producto? producto = await _sistemaGestionContext.Productos
                                        .Include(p => p.Usuario)
                                        .FirstOrDefaultAsync(p => p.Id == id);

        if (producto is null)
        {
            throw new ArgumentException($"No existe el producto con id {id}");
        }

        return producto;
    }

    public async Task<List<Producto>> ObtenerProductosAsync (List<int> ids)
    {
        List<Producto> productos = await _sistemaGestionContext.Productos
                                            .Where(p => ids.Contains(p.Id))
                                            .ToListAsync();

        return productos;
    }

    public async Task CrearProductoAsync (Producto producto)
    {
        await _sistemaGestionContext.Productos.AddAsync(producto);
        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task ModificarProductoAsync (int id, Producto productoActualizado)
    {
        Producto producto = await ObtenerProductoAsync(id);

        producto.Descripcion = productoActualizado.Descripcion;
        producto.Costo = productoActualizado.Costo;
        producto.PrecioVenta = productoActualizado.PrecioVenta;
        producto.Stock = productoActualizado.Stock;
        producto.Usuario = productoActualizado.Usuario;

        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task ModificarProductosAsync ()
    {
        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task EliminarProductoAsync (int id)
    {
        Producto producto = await ObtenerProductoAsync(id);

        _sistemaGestionContext.Productos.Remove(producto);
        await _sistemaGestionContext.SaveChangesAsync();
    }
}
