using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;
public class IngresoDTO
{
    [Required]
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string NombreUsuario { get; set; } = null!;

    [Required]
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string Contraseña { get; set; } = null!;
}
