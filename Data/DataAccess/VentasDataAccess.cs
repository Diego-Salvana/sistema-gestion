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

    public async Task<List<Venta>> ListarVentasAsync ()
    {
        return await _sistemaGestionContext.Ventas
                            .Include(v => v.ProductosVendidos)
                            .Include(v => v.Usuario)
                            .Select(v => VentaDTORespuesta.Crear(v))
                            .ToListAsync();
    }

    public async Task<Venta> ObtenerVentaAsync (int id)
    {
        Venta? venta = await _sistemaGestionContext.Ventas.FirstOrDefaultAsync(v => v.Id == id);

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

    public async Task ModificarVentaAsync (int id, Venta ventaActualizada)
    {
        Venta venta = await ObtenerVentaAsync(id);

        venta.Usuario = ventaActualizada.Usuario;
        venta.Comentario = ventaActualizada.Comentario;
        venta.ProductosVendidos = ventaActualizada.ProductosVendidos;

        await _sistemaGestionContext.SaveChangesAsync();
    }

    public async Task EliminarVentaAsync (int id)
    {
        Venta venta = await ObtenerVentaAsync(id);

        _sistemaGestionContext.Ventas.Remove(venta);
        await _sistemaGestionContext.SaveChangesAsync();
    }
}
