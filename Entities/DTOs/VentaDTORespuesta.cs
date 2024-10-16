namespace Entities.DTOs;
public class VentaDTORespuesta
{
    public int Id { get; set; }
    public string Comentario { get; set; }
    public List<Producto> Productos { get; set; }
    public Usuario Usuario { get; set; }

    public VentaDTORespuesta (Venta venta)
    {
        Id = venta.Id;
        Comentario = venta.Comentario;
        Productos = venta.ProductosVendidos.Select(pv => new Producto()
        {
            Id = pv.Producto.Id,
            Descripcion = pv.Producto.Descripcion,
            Costo = pv.Producto.Costo,
            PrecioVenta = pv.Producto.PrecioVenta,
            Stock = pv.Producto.Stock,
        }).ToList();
        Usuario = venta.Usuario;

        Usuario.Contraseña = string.Empty;
    }
}
