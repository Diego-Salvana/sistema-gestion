using Data.Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        ProductoVendido? productoVendido =
            await _sistemaGestionContext.ProductosVendidos.FirstOrDefaultAsync(p => p.Id == id);

        if (productoVendido is null)
        {
            throw new ArgumentException("No existe el producto vendido");
        }

        return productoVendido;
    }

    public async Task<List<ProductoVendido>> ObtenerProductosVendidosAsync (List<int> ids)
    {
        List<ProductoVendido> productos = await _sistemaGestionContext.ProductosVendidos
                                            .Where(p => ids.Contains(p.Producto.Id))
                                            .ToListAsync();

        return productos;
    }

    public async Task CrearProductoVendidoAsync (ProductoVendido productoVendido)
    {
        await _sistemaGestionContext.ProductosVendidos.AddAsync(productoVendido);
        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task ModificarProductoVendidoAsync (int id, ProductoVendido productoAcutalizado)
    {
        ProductoVendido productoVendido = await ObtenerProductoVendidoAsync(id);

        productoVendido.Producto = productoAcutalizado.Producto;
        productoVendido.Stock = productoAcutalizado.Stock;
        productoVendido.Ventas = productoAcutalizado.Ventas;

        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task EliminarProductoVendidoAsync (int id)
    {
        ProductoVendido productoVendido = await ObtenerProductoVendidoAsync(id);

        _sistemaGestionContext.ProductosVendidos.Remove(productoVendido);
        await _sistemaGestionContext.SaveChangesAsync();
    }
}
