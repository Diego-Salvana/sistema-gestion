using System.ComponentModel.DataAnnotations;

namespace Entities;
public class ProductoVendido
{
    public int Id { get; set; }

    public Producto Producto { get; set; } = null!;

    [Required(ErrorMessage = "El campo es requerido.")]
    [Range(0, int.MaxValue, ErrorMessage = "El Stock debe ser mayor o igual a 0.")]
    public int Stock { get; set; }

    public Venta Venta { get; set; } = null!;
}
