namespace Entities.DTOs;
public class VentaDTO
{
    public string Comentario { get; set; } = null!;

    public List<DetalleProducto> ProductosDetalle { get; set; } = [];

    public class DetalleProducto
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
    }

    public class ComentarioTxt
    {
        public string Comentario { get; set; } = null!;
    }
}

