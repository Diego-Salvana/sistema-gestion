using Data.Context;
using Entities;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess;
public class VentasDataAccess
{
    private readonly SistemaGestionContext _sistemaGestionContext;

    public VentasDataAccess (SistemaGestionContext context)
    {
        _sistemaGestionContext = context;
    }

    public async Task<List<VentaDTORespuesta>> ListarVentasAsync ()
    {
        var ventas = await _sistemaGestionContext.Ventas
                                    .Include(v => v.ProductosVendidos)
                                    .ThenInclude(pv => pv.Producto)
                                    .Include(v => v.Usuario)
                                    .Select(v => new VentaDTORespuesta(v))
                                    .ToListAsync();

        return ventas;
    }

    public async Task<List<VentaDTORespuesta>> ListarVentasAsync (int usuarioId)
    {
        var ventas = await _sistemaGestionContext.Ventas
                                    .Include(v => v.ProductosVendidos)
                                    .ThenInclude(pv => pv.Producto)
                                    .Include(v => v.Usuario)
                                    .Where(v => v.Usuario.Id == usuarioId)
                                    .Select(v => new VentaDTORespuesta(v))
                                    .ToListAsync();

        return ventas;
    }

    public async Task<Venta> ObtenerVentaAsync (int ventaId)
    {
        Venta? venta = await _sistemaGestionContext.Ventas
                                    .Include(v => v.ProductosVendidos)
                                    .ThenInclude(pv => pv.Producto)
                                    .Include(v => v.Usuario)
                                    .FirstOrDefaultAsync(v => v.Id == ventaId);

        if (venta is null)
        {
            throw new ArgumentException("No existe la venta");
        }

        return venta;
    }

    public async Task<Venta> CrearVentaAsync (Venta venta)
    {
        await _sistemaGestionContext.Ventas.AddAsync(venta);
        await _sistemaGestionContext.SaveChangesAsync();

        return venta;
    }

    public async Task ModificarVentaAsync (
        int ventaId,
        int usuarioId,
        VentaDTO.ComentarioTxt ventaDTO)
    {
        Venta venta = await ObtenerVentaAsync(ventaId);

        if (venta.Usuario.Id != usuarioId)
        {
            throw new ArgumentException($"La venta no corresponde al usuario con Id {usuarioId}");
        }

        venta.Comentario = ventaDTO.Comentario;

        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task AgregarProducto (
        int ventaId,
        int usuarioId,
        ProductoVendido productoVendido)
    {
        Venta venta = await ObtenerVentaAsync(ventaId);

        if (venta.Usuario.Id != usuarioId)
        {
            throw new ArgumentException($"La venta no corresponde al usuario con Id {usuarioId}");
        }

        productoVendido.Venta = venta;
        productoVendido.Producto.Stock -= productoVendido.Stock;
        await _sistemaGestionContext.ProductosVendidos.AddAsync(productoVendido);

        venta.ProductosVendidos.Add(productoVendido);

        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task QuitarProducto (
        int ventaId,
        int usuarioId,
        ProductoVendido productoVendido)
    {
        if (productoVendido.Venta.Id != ventaId)
        {
            throw new ArgumentException($"El producto no corresponde a la venta con Id {ventaId}");
        }

        Venta venta = await ObtenerVentaAsync(ventaId);

        if (venta.Usuario.Id != usuarioId)
        {
            throw new ArgumentException($"La venta no corresponde al usuario con Id {usuarioId}");
        }

        venta.ProductosVendidos.Remove(productoVendido);
        productoVendido.Producto.Stock += productoVendido.Stock;

        _sistemaGestionContext.ProductosVendidos.Remove(productoVendido);
        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task EliminarVentaAsync (int ventaId, int usuarioId)
    {
        Venta venta = await ObtenerVentaAsync(ventaId);
        
        if (venta.Usuario.Id != usuarioId)
        {
            throw new ArgumentException($"La venta no corresponde al usuario con Id {usuarioId}");
        }

        foreach (ProductoVendido pv in venta.ProductosVendidos)
        {
            pv.Producto.Stock += pv.Stock;
        }

        _sistemaGestionContext.Ventas.Remove(venta);
        await _sistemaGestionContext.SaveChangesAsync();
    }
}
