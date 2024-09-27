using System.ComponentModel.DataAnnotations;

namespace Entities;
public class Venta
{
    public int Id { get; set; }

    public List<string> Comentarios { get; set; } = [];

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El IdUsuario debe ser mayor a 0.")]
    public int IdUsuario { get; set; }

    public void AgregarComentario (string comentario)
    {
        Comentarios.Add(comentario);
    }
}
