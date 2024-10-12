namespace Entities.DTOs;
public class ProductoDTORespuesta
{
    public static Producto Crear (Producto producto)
    {
        producto.Usuario.Contraseña = string.Empty;

        return producto;
    }
}
