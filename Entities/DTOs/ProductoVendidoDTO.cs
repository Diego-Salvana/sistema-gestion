namespace Entities.DTOs;
public class ProductoVendidoDTO
{
    public int Id { get; set; }
    public Producto Producto { get; set; } = null!;
    public int Stock { get; set; }
    public VentaDTORespuesta Venta { get; set; } = null!;

    public ProductoVendidoDTO () { }

    public ProductoVendidoDTO (ProductoVendido pv)
    {
        Id = pv.Id;
        Producto = pv.Producto;
        Stock = pv.Stock;
        Venta = new VentaDTORespuesta(pv.Venta);
    }
}
