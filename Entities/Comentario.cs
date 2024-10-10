using System.ComponentModel.DataAnnotations;

namespace Entities;
public class Comentario
{
    public int Id { get; set; }

    [Required]
    public string Texto { get; set; } = null!;
}
