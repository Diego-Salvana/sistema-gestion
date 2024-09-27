using System.ComponentModel.DataAnnotations;

namespace Entities;
public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Descripción es requerido.")]
    [MaxLength(250, ErrorMessage = "La Descripción no puede tener más de 250 caracteres.")]
    [MinLength(5, ErrorMessage = "La Descripción debe tener al menos 5 caracteres.")]
    public string Descripcion { get; set; } = null!;

    [Required(ErrorMessage = "El campo Costo es requerido.")]
    [Range(0, double.MaxValue, ErrorMessage = "El Costo debe ser mayor o igual a 0.")]
    public decimal Costo { get; set; }

    [Required(ErrorMessage = "El campo Precio de Venta es requerido.")]
    [Range(0, double.MaxValue, ErrorMessage = "El Precio de Venta debe ser mayor o igual a 0.")]
    public decimal PrecioVenta { get; set; }

    [Required(ErrorMessage = "El campo Stock es requerido.")]
    [Range(0, int.MaxValue, ErrorMessage = "El Stock debe ser mayor o igual a 0.")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "El campo Id Usuario es requerido.")]
    [Range(1, int.MaxValue, ErrorMessage = "El Id Usuario debe ser mayor a 0.")]
    public int IdUsuario { get; set; }
}
