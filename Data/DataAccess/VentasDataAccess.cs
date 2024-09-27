using Data.Context;
using Entities;

namespace Data.DataAccess;
public class VentasDataAccess
{
    private readonly SistemaGestionContext _sistemaGestionContext;

    public VentasDataAccess (SistemaGestionContext context)
    {
        _sistemaGestionContext = context;
    }

    public List<Venta> ListarVentas ()
    {
        return _sistemaGestionContext.Ventas.ToList();
    }

    public Venta ObtenerVenta (int id)
    {
        Venta? venta = _sistemaGestionContext.Ventas.FirstOrDefault(v => v.Id == id);

        if (venta is null)
        {
            throw new ArgumentException("No existe la Venta");
        }

        return venta;
    }

    public void CrearVenta (Venta venta)
    {
        _sistemaGestionContext.Ventas.Add(venta);
        _sistemaGestionContext.SaveChanges();
    }

    public void ModificarVenta (int id, Venta ventaAcutalizada)
    {
        Venta venta = ObtenerVenta(id);

        venta.IdUsuario = ventaAcutalizada.IdUsuario;
        venta.Comentarios = ventaAcutalizada.Comentarios;

        _sistemaGestionContext.SaveChanges();
    }

    public void EliminarVenta (int id)
    {
        _sistemaGestionContext.Ventas.Remove(ObtenerVenta(id));
        _sistemaGestionContext.SaveChanges();
    }
}
