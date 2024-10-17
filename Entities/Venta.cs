using System.ComponentModel.DataAnnotations;

namespace Entities;
public class Venta
{
    [Key]
    public int Id { get; set; }

    public string Comentario { get; set; } = null!;
    
    public List<ProductoVendido> ProductosVendidos { get; set; } = [];

    public Usuario Usuario { get; set; } = null!;
}
