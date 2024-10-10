namespace Entities.DTOs;
public class VentaDTO
{
    public List<DetalleProducto> ProductosDetalle { get; set; } = [];

    public int UsuarioId { get; set; }

    public class DetalleProducto
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
    }
}
