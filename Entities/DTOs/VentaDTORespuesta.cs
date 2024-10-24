using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;
public class VentaDTORespuesta
{
    public int Id { get; set; }
    
    [Required]
    public string Comentario { get; set; } = null!;
    public List<ProductoVendido> ProductosVendidos { get; set; } = null!;
    public Usuario Usuario { get; set; } = null!;

    public VentaDTORespuesta () { }

    public VentaDTORespuesta (Venta venta)
    {
        Id = venta.Id;
        Comentario = venta.Comentario;
        ProductosVendidos = venta.ProductosVendidos.Select(pv => new ProductoVendido()
        {
            Id = pv.Id,
            Producto = pv.Producto,
            Stock = pv.Stock
        })
        .ToList();
        Usuario = venta.Usuario;

        Usuario.Contraseña = string.Empty;
    }
}
