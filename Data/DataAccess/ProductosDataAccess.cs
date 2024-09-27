using Data.Context;
using Entities;

namespace Data.DataAccess;
public class ProductosDataAccess
{
    private readonly SistemaGestionContext _sistemaGestionContext;

    public ProductosDataAccess (SistemaGestionContext context)
    {
        _sistemaGestionContext = context;
    }

    public List<Producto> ListarProductos ()
    {
        return _sistemaGestionContext.Productos.ToList();
    }

    public Producto ObtenerProducto (int id)
    {
        Producto? producto = _sistemaGestionContext.Productos.FirstOrDefault(p => p.Id == id);

        if (producto is null)
        {
            throw new ArgumentException("No existe el Producto");
        }

        return producto;
    }

    public void CrearProducto (Producto producto)
    {
        _sistemaGestionContext.Productos.Add(producto);
        _sistemaGestionContext.SaveChanges();
    }

    public void ModificarProducto (int id, Producto productoAcutalizado)
    {
        Producto producto = ObtenerProducto(id);

        producto.Descripcion = productoAcutalizado.Descripcion;
        producto.Costo = productoAcutalizado.Costo;
        producto.PrecioVenta = productoAcutalizado.PrecioVenta;
        producto.Stock = productoAcutalizado.Stock;
        producto.IdUsuario = productoAcutalizado.IdUsuario;

        _sistemaGestionContext.SaveChanges();
    }

    public void EliminarProducto (int id)
    {
        _sistemaGestionContext.Productos.Remove(ObtenerProducto(id));
        _sistemaGestionContext.SaveChanges();
    }
}
