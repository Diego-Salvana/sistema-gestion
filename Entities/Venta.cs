using System.ComponentModel.DataAnnotations;

namespace Entities;
public class Venta
{
    [Key]
    public int Id { get; set; }

    public List<Comentario> Comentarios { get; set; } = [];

    public List<Producto> Productos { get; set; } = [];

    public Usuario Usuario { get; set; } = null!;

    public List<ProductoVendido> ProductoVendidos { get; set; } = [];

    public void AgregarComentario (string comentario)
    {
        //if (comentario.Trim().Length > 0)
        //{
        //    Comentarios.Add(comentario);
        //}
    }
}
