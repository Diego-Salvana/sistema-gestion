using System.ComponentModel.DataAnnotations;

namespace Entities;
public class Usuario
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required]
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string Apellido { get; set; } = null!;

    [Required]
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string NombreUsuario { get; set; } = null!;

    [Required]
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string Contraseña { get; set; } = null!;

    [Required]
    [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    [EmailAddress]
    public string Mail { get; set; } = null!;
}
