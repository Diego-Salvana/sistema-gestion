using Bussiness.Utils;
using Data.DataAccess;
using Entities;

namespace Bussiness.Services;
public class ProductoVendidoBussiness
{
    private readonly ProductosVendidosDataAccess _productosVendidosDataAcces;

    public ProductoVendidoBussiness (ProductosVendidosDataAccess dataAccess)
    {
        _productosVendidosDataAcces = dataAccess;
    }

    public List<ProductoVendido> ListarProductosVendidos ()
    {
        try
        {
            return _productosVendidosDataAcces.ListarProductosVendidos();
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener ProductosVendidos");
        }
    }

    public ProductoVendido ObtenerProductoVendido (int id)
    {
        ProductoVendido? productoVendido;

        try
        {
            productoVendido = _productosVendidosDataAcces.ObtenerProductoVendido(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener el ProductoVendido");
        }

        return productoVendido;
    }

    public void CrearProductoVendido (ProductoVendido productoVendido)
    {
        try
        {
            _productosVendidosDataAcces.CrearProductoVendido(productoVendido);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al crear el ProductoVendido");
        }
    }

    public void ModificarProductoVendido (int id, ProductoVendido productoAcutalizado)
    {
        try
        {
            _productosVendidosDataAcces.ModificarProductoVendido(id, productoAcutalizado);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al modificar el ProductoVendido");
        }
    }

    public void EliminarProductoVendido (int id)
    {
        try
        {
            _productosVendidosDataAcces.EliminarProductoVendido(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar el ProductoVendido");
        }
    }
}
