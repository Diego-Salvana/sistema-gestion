using Bussiness.Utils;
using Data.DataAccess;
using Entities;

namespace Bussiness.Services;
public class VentaBussiness
{
    private readonly VentasDataAccess _ventasDataAccess;

    public VentaBussiness (VentasDataAccess dataAccess)
    {
        _ventasDataAccess = dataAccess;
    }

    public List<Venta> ListarVentas ()
    {
        try
        {
            return _ventasDataAccess.ListarVentas();
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener Ventas");
        }
    }

    public Venta ObtenerVenta (int id)
    {
        Venta? venta;

        try
        {
            venta = _ventasDataAccess.ObtenerVenta(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener la Venta");
        }

        return venta;
    }

    public void CrearVenta (Venta venta)
    {
        try
        {
            _ventasDataAccess.CrearVenta(venta);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al crear la Venta");
        }
    }

    public void ModificarVenta (int id, Venta ventaAcutalizada)
    {
        try
        {
            _ventasDataAccess.ModificarVenta(id, ventaAcutalizada);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al modificar la Venta");
        }
    }

    public void EliminarVenta (int id)
    {
        try
        {
            _ventasDataAccess.EliminarVenta(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar la Venta");
        }
    }
}
