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

    public async Task<Venta> ObtenerVentaAsync (int ventaId, int usuarioId)
    {
        Venta? venta = await _sistemaGestionContext.Ventas
                                    .Include(v => v.ProductosVendidos)
                                    .ThenInclude(pv => pv.Producto)
                                    .Include(v => v.Usuario)
                                    .Where(v => v.Usuario.Id == usuarioId)
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
        Venta venta = await ObtenerVentaAsync(ventaId, usuarioId);

        venta.Comentario = ventaDTO.Comentario;

        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task EliminarVentaAsync (int ventaId, int usuarioId)
    {
        Venta venta = await ObtenerVentaAsync(ventaId, usuarioId);

        _sistemaGestionContext.Ventas.Remove(venta);
        await _sistemaGestionContext.SaveChangesAsync();
    }
}
