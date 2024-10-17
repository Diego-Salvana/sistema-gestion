namespace Entities.DTOs;
public class ProductoVendidoDTO
{
    public int Id { get; set; }
    public Producto Producto { get; set; }
    public VentaDTORespuesta Venta { get; set; }

    public ProductoVendidoDTO (ProductoVendido pv)
    {
        Id = pv.Id;
        Producto = pv.Producto;
        Venta = new VentaDTORespuesta(pv.Venta);
        
        Producto.Stock = pv.Stock;
    }
}
