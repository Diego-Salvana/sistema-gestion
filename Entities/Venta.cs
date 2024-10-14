using System.ComponentModel.DataAnnotations;

namespace Entities;
public class Venta
{
    [Key]
    public int Id { get; set; }

    public string Comentario { get; set; } = null!;

    public List<Producto> Productos { get; set; } = [];

    public Usuario Usuario { get; set; } = null!;

    public void AgregarComentario (string comentario)
    {
        //if (comentario.Trim().Length > 0)
        //{
        //    Comentarios.Add(comentario);
        //}
    }
}
