namespace Entities.DTOs;
public class VentaDTORespuesta
{
    public static Venta Crear (Venta venta)
    {
        //venta.ProductosVendidos = venta.ProductosVendidos
        //    .Select(p => new ProductoVe()
        //{
        //    Id = p.Id,
        //    Stock = p.Stock,
        //    Usuario = { },
        //}).ToList();

        venta.Usuario.Contraseña = string.Empty;

        return venta;
    }
}
