using Bussiness.Utils;
using Data.DataAccess;
using Entities;

namespace Bussiness.Services;
public class ProductoBussiness
{
    private readonly ProductosDataAccess _productosDataAccess;

    public ProductoBussiness (ProductosDataAccess dataAccess)
    {
        _productosDataAccess = dataAccess;
    }

    public List<Producto> ListarProductos ()
    {
        try
        {
            return _productosDataAccess.ListarProductos();
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió error al obtener Productos");
        }
    }

    public Producto ObtenerProducto (int id)
    {
        Producto? producto;

        try
        {
            producto = _productosDataAccess.ObtenerProducto(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener el Producto");
        }

        return producto;
    }

    public void CrearProducto (Producto producto)
    {
        try
        {
            _productosDataAccess.CrearProducto(producto);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al crear el Producto");
        }
    }

    public void ModificarProducto (int id, Producto productoAcutalizado)
    {
        try
        {
            _productosDataAccess.ModificarProducto(id, productoAcutalizado);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al modificar el Producto");
        }
    }

    public void EliminarProducto (int id)
    {
        try
        {
            _productosDataAccess.EliminarProducto(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar el Producto");
        }
    }
}
