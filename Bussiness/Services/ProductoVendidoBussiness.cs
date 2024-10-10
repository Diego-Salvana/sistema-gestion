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

    public async Task<List<ProductoVendido>> ListarProductosVendidosAsync ()
    {
        try
        {
            return await _productosVendidosDataAcces.ListarProductosVendidosAsync();
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener ProductosVendidos");
        }
    }

    public async Task<ProductoVendido> ObtenerProductoVendidoAsync (int id)
    {
        try
        {
            return await _productosVendidosDataAcces.ObtenerProductoVendidoAsync(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al obtener el ProductoVendido");
        }
    }

    public async Task CrearProductoVendidoAsync (ProductoVendido productoVendido)
    {
        try
        {
            await _productosVendidosDataAcces.CrearProductoVendidoAsync(productoVendido);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al crear el ProductoVendido");
        }
    }

    public async Task ModificarProductoVendidoAsync (int id, ProductoVendido productoAcutalizado)
    {
        try
        {
            await _productosVendidosDataAcces.ModificarProductoVendidoAsync(id, productoAcutalizado);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al modificar el ProductoVendido");
        }
    }

    public async Task EliminarProductoVendidoAsync (int id)
    {
        try
        {
            await _productosVendidosDataAcces.EliminarProductoVendidoAsync(id);
        }
        catch (Exception ex)
        {
            throw ErrorHandler.Error(ex, "Ocurrió un error al eliminar el ProductoVendido");
        }
    }
}
