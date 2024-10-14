namespace Entities.DTOs;
public class ProductoDTO
{
    public string Descripcion { get; set; } = string.Empty;
    public decimal Costo { get; set; }
    public decimal PrecioVenta { get; set; }
    public int Stock { get; set; }
    public int UsuarioId { get; set; }
}
