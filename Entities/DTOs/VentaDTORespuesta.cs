namespace Entities.DTOs;
public class VentaDTORespuesta
{
    public static Venta Crear (Venta venta)
    {
        venta.Productos = venta.Productos.Select(p => new Producto()
        {
            Id = p.Id,
            Descripcion = p.Descripcion,
            Costo = p.Costo,
            Stock = p.Stock,
            PrecioVenta = p.PrecioVenta,
            Usuario = { },
        }).ToList();

        venta.Usuario.Contraseña = string.Empty;

        return venta;
    }
}
